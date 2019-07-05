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

    public class PackageReader
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

        private async Task PreChunkFileProcess(IEnumerable<IPackageProcessor> packageProcessors, string chunkFullFilename)
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors)
                await fileProcessor.PreChunkFileProcess(chunkFullFilename);
        }

        private async Task ProcessChunkFile(PackageChildEntry childEntry, IEnumerable<IPackageProcessor> matchingPackageProcessors)
        {
            foreach (IPackageProcessor packageFileProcessor in matchingPackageProcessors)
            {
                cachedSubStream.Initialize(reader.BaseStream, childEntry.FileOffset, childEntry.FileSize);
                await packageFileProcessor.ProcessChunkFile(cachedSubStream, childEntry.ChunkFullFilename);
            }
        }

        private async Task PostChunkFileProcess(IEnumerable<IPackageProcessor> packageProcessors, string chunkFullFilename)
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors.Reverse())
                await fileProcessor.PostChunkFileProcess(chunkFullFilename);
        }

        private int ReadHeader()
        {
            reader.BaseStream.Seek(0x0C, SeekOrigin.Begin);
            int totalParentCount = reader.ReadInt32();
            reader.BaseStream.Seek(0x100, SeekOrigin.Begin);

            return totalParentCount;
        }

        private async Task RunMatchingPackageProcessors(PackageChildEntry childEntry, IEnumerable<IPackageProcessor>  matchingPackageProcessors)
        {
            await PreChunkFileProcess(matchingPackageProcessors, childEntry.ChunkFullFilename);

            long savedPosition = reader.BaseStream.Position;
            try
            {
                await ProcessChunkFile(childEntry, matchingPackageProcessors);
            }
            finally
            {
                reader.BaseStream.Seek(savedPosition, SeekOrigin.Begin);
            }

            await PostChunkFileProcess(matchingPackageProcessors, childEntry.ChunkFullFilename);
        }

        private async Task ProcessChildEntry()
        {
            PackageChildEntry childEntry = PackageChildEntry.Read(reader);

            if (childEntry.EntryType == 1) // Folder
                return;

            if (childEntry.EntryType != 0 && childEntry.EntryType != 2)
            {
                logger?.LogError($"Found file in chunk but unexpected entry type (expected 0, actual is {childEntry.EntryType})");
                return;
            }

            IEnumerable<IPackageProcessor> matchingPackageProcessors = packageProcessors
                .Where(x => x.IsChunkFileMatching(childEntry.ChunkFullFilename));

            await RunMatchingPackageProcessors(childEntry, matchingPackageProcessors);
        }

        private async Task ProcessParentEntry()
        {
            PackageParentEntry parentEntry = PackageParentEntry.Read(reader);

            for (int j = 0; j < parentEntry.ChildCount; j++)
                await ProcessChildEntry();
        }

        private async Task PreProcess()
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors)
                await fileProcessor.PreProcess();
        }

        private async Task ProcessPackageFile(string packageFullFilename)
        {
            using (reader = new BinaryReader(File.OpenRead(packageFullFilename), Encoding.UTF8, false))
            {
                int totalParentCount = ReadHeader();

                for (int i = 0; i < totalParentCount; i++)
                    await ProcessParentEntry();
            }
        }

        private async Task PostProcess()
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors.Reverse())
                await fileProcessor.PostProcess();
        }

        public async Task Run(string packagesFullPath)
        {
            IEnumerable<string> packageFilenames = Directory.GetFiles(packagesFullPath, "chunk*.pkg", SearchOption.TopDirectoryOnly)
                .Select(x => new { OriginalFilename = x, Index = PackageUtility.GetChunkFileIndex(x) })
                .Where(x => x.Index >= 0)
                .OrderByDescending(x => x.Index)
                .Select(x => x.OriginalFilename);

            await PreProcess();

            foreach (string packageFilename in packageFilenames)
                await ProcessPackageFile(packageFilename);

            await PostProcess();
        }
    }
}
