using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public class PackageUtility
    {
        public static string GetPackagesFullPath(ILogger logger = null)
        {
            string packageFullPathStoreFile = Path.Combine(AppContext.BaseDirectory, "packages_path.txt");

            if (File.Exists(packageFullPathStoreFile))
            {
                string[] lines = File.ReadAllLines(packageFullPathStoreFile);

                if (lines != null)
                {
                    string result = lines.FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false)?.Trim();
                    if (result != null)
                    {
                        if (Directory.Exists(result))
                        {
                            logger?.LogTrace($"Found from '{packageFullPathStoreFile}': {result}");
                            return result;
                        }
                        else
                            logger?.LogWarning($"'{packageFullPathStoreFile}' seems to not exists");
                    }
                    else
                        logger?.LogWarning($"File '{packageFullPathStoreFile}' contains invalid data");
                }
                else
                    logger?.LogWarning($"ReadAllLines of file '{packageFullPathStoreFile}' returned null");
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter path to chunk*.pkg files: (Ctrl+C to quit)");

                string path = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(path))
                    continue;

                if (Directory.Exists(path) == false)
                {
                    Console.WriteLine("Not found, please retry");
                    continue;
                }

                File.WriteAllText(packageFullPathStoreFile, path);

                logger?.LogTrace($"Obtained from termainl input, and saved to '{packageFullPathStoreFile}': {path}");

                return path;
            }
        }

        public static async Task ProcessPackageFile(ILogger logger, string packageFullFilename, IEnumerable<IPackageProcessor> packageProcessors)
        {
            foreach (IPackageProcessor fileProcessor in packageProcessors)
                await fileProcessor.PrePackageFileProcess(packageFullFilename);

            var subStream = new SubStream();

            using (var reader = new BinaryReader(File.OpenRead(packageFullFilename), Encoding.UTF8, false))
            {
                reader.BaseStream.Seek(0x0C, SeekOrigin.Begin);
                int totalParentCount = reader.ReadInt32();
                int totalChildrenCount = reader.ReadInt32();
                reader.BaseStream.Seek(0x100, SeekOrigin.Begin);

                byte[] nameBuffer = new byte[160];

                for (int i = 0; i < totalParentCount; i++)
                {
                    reader.BaseStream.Seek(0x3C, SeekOrigin.Current);

                    long fileSize = reader.ReadInt64();
                    long fileOffset = reader.ReadInt64();
                    int entryType = reader.ReadInt32();
                    int childrenCount = reader.ReadInt32();

                    for (int j = 0; j < childrenCount; j++)
                    {
                        reader.Read(nameBuffer, 0, nameBuffer.Length);
                        fileSize = reader.ReadInt64();
                        fileOffset = reader.ReadInt64();
                        entryType = reader.ReadInt32();
                        reader.BaseStream.Seek(4, SeekOrigin.Current);

                        int trailingZeroIndex = Array.IndexOf<byte>(nameBuffer, 0);
                        string chunkFullFilename = Encoding.UTF8.GetString(nameBuffer, 0, trailingZeroIndex);

                        if (entryType == 1) // Folder
                            continue;

                        if (entryType != 0 && entryType != 2)
                        {
                            logger?.LogError($"Found file in chunk but unexpected entry type (expected 0, actual is {entryType})");
                            return;
                        }

                        IEnumerable<IPackageProcessor> matchingPackageProcessors = packageProcessors.Where(x => x.IsChunkFileMatching(chunkFullFilename));

                        foreach (IPackageProcessor fileProcessor in matchingPackageProcessors)
                            await fileProcessor.PreChunkFileProcess(chunkFullFilename);

                        long savedPosition = reader.BaseStream.Position;

                        foreach (IPackageProcessor packageFileProcessor in matchingPackageProcessors)
                        {
                            subStream.Initialize(reader.BaseStream, fileOffset, fileSize);
                            await packageFileProcessor.ProcessChunkFile(subStream, chunkFullFilename);
                        }

                        reader.BaseStream.Seek(savedPosition, SeekOrigin.Begin);

                        foreach (IPackageProcessor fileProcessor in matchingPackageProcessors.Reverse())
                            await fileProcessor.PostChunkFileProcess(chunkFullFilename);
                    }
                }
            }

            foreach (IPackageProcessor fileProcessor in packageProcessors.Reverse())
                await fileProcessor.PostPackageFileProcess(packageFullFilename);
        }

        public static async Task RunPackageProcessors(ILogger logger, string packagesFullPath, IEnumerable<IPackageProcessor> packageProcessors)
        {
            IEnumerable<string> packageFilenames = Directory.GetFiles(packagesFullPath, "chunk*.pkg", SearchOption.TopDirectoryOnly)
                .Select(x => new { OriginalFilename = x, Index = GetChunkFileIndex(x) })
                .Where(x => x.Index >= 0)
                .OrderByDescending(x => x.Index)
                .Select(x => x.OriginalFilename);

            foreach (IPackageProcessor fileProcessor in packageProcessors)
                await fileProcessor.PreProcess();

            foreach (string packageFilename in packageFilenames)
                await ProcessPackageFile(logger, packageFilename, packageProcessors);

            foreach (IPackageProcessor fileProcessor in packageProcessors.Reverse())
                await fileProcessor.PostProcess();

        }

        public static Regex ChunkIndexRegex = new Regex(@"^chunk(\d+)\.pkg$");

        public static int GetChunkFileIndex(string packageFullPath)
        {
            packageFullPath = Path.GetFileName(packageFullPath);

            Match match = ChunkIndexRegex.Match(packageFullPath);

            if (match.Success)
                return int.Parse(match.Groups[1].Value);

            return -1;
        }
    }
}
