using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public abstract class PackageProcessorBase : IPackageProcessor
    {
        public virtual Task PreProcess()
        {
            return Task.CompletedTask;
        }

        public virtual Task PrePackageFileProcess(string packageFullFilename)
        {
            return Task.CompletedTask;
        }

        public virtual Task PreChunkFileProcess(string chunkFullFilename)
        {
            return Task.CompletedTask;
        }

        public abstract bool IsChunkFileMatching(string chunkFullFilename);
        public abstract Task ProcessChunkFile(Stream stream, string chunkFullFilename);

        public virtual Task PostChunkFileProcess(string chunkFullFilename)
        {
            return Task.CompletedTask;
        }

        public virtual Task PostPackageFileProcess(string packageFullFilename)
        {
            return Task.CompletedTask;
        }

        public virtual Task PostProcess()
        {
            return Task.CompletedTask;
        }
    }
}
