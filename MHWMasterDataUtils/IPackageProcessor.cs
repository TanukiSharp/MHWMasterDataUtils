using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public interface ICryptoInfo
    {
        string Key { get; }
    }

    public class CryptoInfo : ICryptoInfo
    {
        public string Key { get; private set; }

        public CryptoInfo(string key)
        {
            Key = key;
        }
    }

    public interface IPackageProcessor
    {
        ICryptoInfo Crypto { get; }
        void PreProcess();
        void PreChunkFileProcess(string chunkFullFilename);
        bool IsChunkFileMatching(string chunkFullFilename);
        void ProcessChunkFile(Stream stream, string chunkFullFilename);
        void PostChunkFileProcess(string chunkFullFilename);
        void PostProcess();
    }
}
