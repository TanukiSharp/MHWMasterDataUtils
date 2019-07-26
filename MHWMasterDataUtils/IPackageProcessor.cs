using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public interface IPackageProcessor
    {
        void PreProcess();
        void PreChunkFileProcess(string chunkFullFilename);
        bool IsChunkFileMatching(string chunkFullFilename);
        void ProcessChunkFile(Stream stream, string chunkFullFilename);
        void PostChunkFileProcess(string chunkFullFilename);
        void PostProcess();
    }
}
