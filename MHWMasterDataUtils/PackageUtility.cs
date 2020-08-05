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

        public static Regex ChunkIndexRegex = new Regex(@"^chunkG(\d+)\.pkg$");

        public static int GetChunkFileIndex(string packageFullPath)
        {
            packageFullPath = Path.GetFileName(packageFullPath);

            Match match = ChunkIndexRegex.Match(packageFullPath);

            if (match.Success)
                return int.Parse(match.Groups[1].Value);

            return -1;
        }

        private const uint IceborneHeaderValue = 0x18091001;

        public static void ReadAndAssertIceborneHeader(Reader reader)
        {
            uint iceborneHeaderValue = reader.ReadUInt32();

            if (iceborneHeaderValue != IceborneHeaderValue)
                throw new FormatException($"Invalid meh value '{reader.Filename ?? "<unknown>"}'. Expected 0x{IceborneHeaderValue:X04}, read 0x{iceborneHeaderValue:X04}.");
        }

        public static void ReadAndAssertTwoBytesHeader(ushort[] allowedValues, Reader reader)
        {
            ReadAndAssertTwoBytesHeader(allowedValues, null, reader);
        }

        public static void ReadAndAssertTwoBytesHeader(ushort[] allowedValues, ushort[] skipValues, Reader reader)
        {
            ushort headerValue = reader.ReadUInt16();

            if (skipValues != null && skipValues.Contains(headerValue))
                throw new SkipException($"Header value: 0x{headerValue:X04}");

            if (allowedValues.Contains(headerValue) == false)
                throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {string.Join(", ", allowedValues.Select(x => $"0x{x:X04}"))}, read 0x{headerValue:X04}.");
        }
    }
}
