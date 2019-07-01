using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Weapons
{
    public class BottleTablePackageProcessor : PackageProcessorBase
    {
        public override bool IsChunkFileMatching(string filename)
        {
            return filename == "\\common\\equip\\bottle_table.bbtbl";
        }

        public override Task ProcessChunkFile(Stream stream, string packageFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), packageFilename))
                return Process(reader);
        }

        private Task Process(Reader reader)
        {
            return Task.CompletedTask;
        }
    }
}
