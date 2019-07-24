using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Tester
{
    public class DumpPackageProcessor : PackageProcessorBase
    {
        private readonly string matchingFile;
        private bool isDumped;

        public DumpPackageProcessor(string matchingFile)
        {
            this.matchingFile = matchingFile;
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            if (isDumped == false && chunkFullFilename == matchingFile)
            {
                isDumped = true;
                return true;
            }

            return false;
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (Stream fs = File.OpenWrite(Path.Combine(AppContext.BaseDirectory, Path.GetFileName(chunkFullFilename))))
                stream.CopyTo(fs);

            return Task.CompletedTask;
        }
    }
}
