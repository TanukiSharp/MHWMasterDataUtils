using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public interface IPackageProcessor
    {
        Task PreProcess();
        Task PreChunkFileProcess(string chunkFullFilename);
        bool IsChunkFileMatching(string chunkFullFilename);
        Task ProcessChunkFile(Stream stream, string chunkFullFilename);
        Task PostChunkFileProcess(string chunkFullFilename);
        Task PostProcess();
    }
}
