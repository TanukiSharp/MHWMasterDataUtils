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
        public static string GetPackagesStoreFileFullFilename()
        {
            return Path.Combine(AppContext.BaseDirectory, "packages_path.txt");
        }

        private static string FindExistingFolderPathFromStoreFile(string packageFullPathStoreFile, ILogger logger)
        {
            string[] lines = File.ReadAllLines(packageFullPathStoreFile);

            if (lines != null)
            {
                string firstNonEmptyLine = lines
                    .FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false)
                    ?.Trim();

                if (firstNonEmptyLine != null)
                {
                    if (Directory.Exists(firstNonEmptyLine))
                    {
                        logger?.LogTrace($"Found from '{packageFullPathStoreFile}': {firstNonEmptyLine}");
                        return firstNonEmptyLine;
                    }
                    else
                        logger?.LogWarning($"'{packageFullPathStoreFile}' seems to not exists");
                }
                else
                    logger?.LogWarning($"File '{packageFullPathStoreFile}' contains invalid data");
            }
            else
                logger?.LogWarning($"ReadAllLines of file '{packageFullPathStoreFile}' returned null");

            return null;
        }

        private static string PromptUser(ILogger logger, string packageFullPathStoreFile)
        {
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

        public static string GetPackagesFullPath(ILogger logger = null)
        {
            string packagesStoreFileFullFilename = GetPackagesStoreFileFullFilename();

            if (File.Exists(packagesStoreFileFullFilename))
            {
                string existingFolderPath = FindExistingFolderPathFromStoreFile(packagesStoreFileFullFilename, logger);

                if (existingFolderPath != null)
                    return existingFolderPath;
            }

            return PromptUser(logger, packagesStoreFileFullFilename);
        }

        public static async Task RunPackageProcessors(ILogger logger, string packagesFullPath, IEnumerable<IPackageProcessor> packageProcessors)
        {
            var meh = Directory.GetFiles(packagesFullPath, "chunk*.pkg", SearchOption.TopDirectoryOnly);

            IEnumerable<string> packageFilenames = meh
                .Select(x => new { OriginalFilename = x, Index = GetChunkFileIndex(x) })
                .Where(x => x.Index >= 0)
                .OrderByDescending(x => x.Index)
                .Select(x => x.OriginalFilename);

            var packageReader = new PackageReader(logger, packageProcessors);

            await packageReader.Begin();

            foreach (string packageFilename in packageFilenames)
                await packageReader.ProcessPackageFile(packageFilename);

            await packageReader.End();
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
