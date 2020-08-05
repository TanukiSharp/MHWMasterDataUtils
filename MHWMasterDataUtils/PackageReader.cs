using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MHWMasterDataUtils
{
    public struct PackageParentEntry
    {
        public readonly long FileSize;
        public readonly long FileOffset;
        public readonly int EntryType;
        public readonly int ChildCount;

        private PackageParentEntry(long fileSize, long fileOffset, int entryType, int childCount)
        {
            FileSize = fileSize;
            FileOffset = fileOffset;
            EntryType = entryType;
            ChildCount = childCount;
        }

        public static PackageParentEntry Read(BinaryReader reader)
        {
            reader.BaseStream.Seek(0x3C, SeekOrigin.Current);

            long fileSize = reader.ReadInt64();
            long fileOffset = reader.ReadInt64();
            int entryType = reader.ReadInt32();
            int childCount = reader.ReadInt32();

            return new PackageParentEntry(fileSize, fileOffset, entryType, childCount);
        }
    }

    public struct PackageChildEntry
    {
        public readonly long FileSize;
        public readonly long FileOffset;
        public readonly int EntryType;
        public readonly string ChunkFullFilename;

        private static readonly byte[] nameBuffer = new byte[160];

        private PackageChildEntry(long fileSize, long fileOffset, int entryType, string chunkFullFilename)
        {
            FileSize = fileSize;
            FileOffset = fileOffset;
            EntryType = entryType;
            ChunkFullFilename = chunkFullFilename;
        }

        public static PackageChildEntry Read(BinaryReader reader)
        {
            string chunkFullFilename;

            lock (nameBuffer)
            {
                reader.Read(nameBuffer, 0, nameBuffer.Length);
                chunkFullFilename = NativeUtils.GetFirstString(nameBuffer, Encoding.UTF8).Replace('\\', '/');
            }

            long fileSize = reader.ReadInt64();
            long fileOffset = reader.ReadInt64();
            int entryType = reader.ReadInt32();
            reader.BaseStream.Seek(4, SeekOrigin.Current);

            return new PackageChildEntry(fileSize, fileOffset, entryType, chunkFullFilename);
        }
    }

    public class PackageReader : IDisposable
    {
        private readonly ILogger logger;
        private readonly IEnumerable<IPackageProcessor> packageProcessors;

        private BinaryReader reader;
        private readonly SubStream cachedSubStream = new SubStream();

        public PackageReader(ILogger logger, IEnumerable<IPackageProcessor> packageProcessors)
        {
            this.logger = logger;
            this.packageProcessors = packageProcessors;
        }

        private static void PreChunkFileProcess(IEnumerable<IPackageProcessor> packageProcessors, string chunkFullFilename)
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors)
                fileProcessor.PreChunkFileProcess(chunkFullFilename);
        }

        private void ProcessChunkFile(PackageChildEntry childEntry, IEnumerable<IPackageProcessor> matchingPackageProcessors)
        {
            foreach (IPackageProcessor packageFileProcessor in matchingPackageProcessors)
            {
                cachedSubStream.Initialize(reader.BaseStream, childEntry.FileOffset, childEntry.FileSize);

                Stream workStream = cachedSubStream;

                if (packageFileProcessor.Crypto != null)
                {
                    byte[] buffer = new byte[workStream.Length];

                    workStream.Read(buffer);

                    var blowfish = new MHWCrypto.Blowfish(packageFileProcessor.Crypto.Key);
                    blowfish.Decrypt(buffer);

                    workStream = new MemoryStream(buffer);
                }

                try
                {
                    packageFileProcessor.ProcessChunkFile(workStream, childEntry.ChunkFullFilename);
                }
                catch (SkipException ex)
                {
                    logger.LogInformation($"Skipped '{(reader.BaseStream is FileStream fs ? fs.Name : "<unknown>")}' => '{childEntry.ChunkFullFilename}' [{ex.Message}] ");
                }
            }
        }

        private static void PostChunkFileProcess(IEnumerable<IPackageProcessor> packageProcessors, string chunkFullFilename)
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors.Reverse())
                fileProcessor.PostChunkFileProcess(chunkFullFilename);
        }

        private int ReadHeader()
        {
            reader.BaseStream.Seek(0x0C, SeekOrigin.Begin);
            int totalParentCount = reader.ReadInt32();
            reader.BaseStream.Seek(0x0100, SeekOrigin.Begin);

            return totalParentCount;
        }

        private void RunMatchingPackageProcessors(PackageChildEntry childEntry, IEnumerable<IPackageProcessor> matchingPackageProcessors)
        {
            PreChunkFileProcess(matchingPackageProcessors, childEntry.ChunkFullFilename);

            long savedPosition = reader.BaseStream.Position;
            try
            {
                ProcessChunkFile(childEntry, matchingPackageProcessors);
            }
            finally
            {
                reader.BaseStream.Seek(savedPosition, SeekOrigin.Begin);
            }

            PostChunkFileProcess(matchingPackageProcessors, childEntry.ChunkFullFilename);
        }

        private void ProcessChildEntry()
        {
            var childEntry = PackageChildEntry.Read(reader);

            if (childEntry.EntryType == 1) // Folder
                return;

            if (childEntry.EntryType != 0 && childEntry.EntryType != 2)
            {
                logger?.LogError($"Found file in chunk but unexpected entry type (expected 0, actual is {childEntry.EntryType})");
                return;
            }

            IEnumerable<IPackageProcessor> matchingPackageProcessors = packageProcessors
                .Where(x => x.IsChunkFileMatching(childEntry.ChunkFullFilename))
                .ToList(); // <-- Need to freeze the state here, since iterated many times.

            RunMatchingPackageProcessors(childEntry, matchingPackageProcessors);
        }

        private void ProcessParentEntry()
        {
            var parentEntry = PackageParentEntry.Read(reader);

            for (int j = 0; j < parentEntry.ChildCount; j++)
                ProcessChildEntry();
        }

        private void PreProcess()
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors)
                fileProcessor.PreProcess();
        }

        private void ProcessPackageFile(string packageFullFilename)
        {
            using (reader = new BinaryReader(File.OpenRead(packageFullFilename), Encoding.UTF8, false))
            {
                int totalParentCount = ReadHeader();

                for (int i = 0; i < totalParentCount; i++)
                    ProcessParentEntry();
            }
        }

        private void PostProcess()
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors.Reverse())
                fileProcessor.PostProcess();
        }

        public void Run(string packagesFullPath)
        {
            IEnumerable<string> packageFilenames = Directory.GetFiles(packagesFullPath, "chunkG*.pkg", SearchOption.TopDirectoryOnly)
                .Select(x => new { OriginalFilename = x, Index = PackageUtility.GetChunkFileIndex(x) })
                .Where(x => x.Index >= 0)
                .OrderByDescending(x => x.Index)
                .Select(x => x.OriginalFilename);

            PreProcess();

            foreach (string packageFilename in packageFilenames)
                ProcessPackageFile(packageFilename);

            PostProcess();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    cachedSubStream.Dispose();
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
