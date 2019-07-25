using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Tester
{
    public class PrintFilenamePackageProcessor : PackageProcessorBase
    {
        private readonly ILogger logger;

        public PrintFilenamePackageProcessor(ILogger logger)
        {
            this.logger = logger;
        }

        public override Task PrePackageFileProcess(string packageFullFilename)
        {
            logger?.LogInformation($"package file: {packageFullFilename}");
            return base.PrePackageFileProcess(packageFullFilename);
        }

        private readonly HashSet<string> files = new HashSet<string>();

        public override Task PreChunkFileProcess(string fileFullFilename)
        {
            if (fileFullFilename.Contains("rod", StringComparison.OrdinalIgnoreCase))
                files.Add(fileFullFilename);

            return base.PreChunkFileProcess(fileFullFilename);
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return true;
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            return Task.CompletedTask;
        }

        //public override Task PostPackageFileProcess(string packageFullFilename)
        //{
        //    logger?.LogInformation("");
        //    return base.PostChunkFileProcess(packageFullFilename);
        //}

        public override Task PostProcess()
        {
            foreach (string file in files)
                logger?.LogInformation($"  - chunk file: {file}");

            return base.PostProcess();
        }
    }
}
