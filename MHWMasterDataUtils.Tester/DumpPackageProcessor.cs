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

        public DumpPackageProcessor(string matchingFile)
        {
            this.matchingFile = matchingFile;
        }

        private int counter = 0;

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == matchingFile;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            string name = Path.GetFileNameWithoutExtension(chunkFullFilename);
            string ext = Path.GetExtension(chunkFullFilename);
            string filename = Path.Combine(AppContext.BaseDirectory, $"{name}_{counter:d02}{ext}");

            counter++;

            using (Stream fs = File.OpenWrite(filename))
                stream.CopyTo(fs);
        }
    }
}
