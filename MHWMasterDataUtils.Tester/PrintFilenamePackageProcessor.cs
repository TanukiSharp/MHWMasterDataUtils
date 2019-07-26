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

        public override void PrePackageFileProcess(string packageFullFilename)
        {
            logger?.LogInformation($"package file: {packageFullFilename}");
            base.PrePackageFileProcess(packageFullFilename);
        }

        private readonly HashSet<string> files = new HashSet<string>();

        public override void PreChunkFileProcess(string fileFullFilename)
        {
            if (fileFullFilename.Contains("rod", StringComparison.OrdinalIgnoreCase))
                files.Add(fileFullFilename);

            base.PreChunkFileProcess(fileFullFilename);
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return true;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
        }

        public override void PostProcess()
        {
            foreach (string file in files)
                logger?.LogInformation($"  - chunk file: {file}");

            base.PostProcess();
        }
    }
}
