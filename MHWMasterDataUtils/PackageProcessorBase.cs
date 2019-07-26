using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public abstract class PackageProcessorBase : IPackageProcessor
    {
        public virtual void PreProcess()
        {
        }

        public virtual void PrePackageFileProcess(string packageFullFilename)
        {
        }

        public virtual void PreChunkFileProcess(string chunkFullFilename)
        {
        }

        public abstract bool IsChunkFileMatching(string chunkFullFilename);
        public abstract void ProcessChunkFile(Stream stream, string chunkFullFilename);

        public virtual void PostChunkFileProcess(string chunkFullFilename)
        {
        }

        public virtual void PostPackageFileProcess(string packageFullFilename)
        {
        }

        public virtual void PostProcess()
        {
        }
    }
}
