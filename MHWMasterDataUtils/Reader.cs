using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MHWMasterDataUtils
{
    public class Reader : IDisposable
    {
        private readonly BinaryReader innerReader;
        private readonly string packageFile;

        public Reader(BinaryReader innerReader, string packageFile)
        {
            if (innerReader == null)
                throw new ArgumentNullException(nameof(innerReader));

            this.innerReader = innerReader;
            this.packageFile = packageFile;
        }

        public string Filename
        {
            get
            {
                if (innerReader.BaseStream is FileStream fs)
                    return $"{fs.Name}{(packageFile != null ? $" [{packageFile}]" : string.Empty)}";
                if (innerReader.BaseStream is SubStream s)
                    return $"{s.Filename}{(packageFile != null ? $" [{packageFile}]" : string.Empty)}";

                return null;
            }
        }

        public long Position
        {
            get
            {
                return innerReader.BaseStream.Position;
            }
        }

        public byte ReadByte()
        {
            return innerReader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return innerReader.ReadSByte();
        }

        public byte[] ReadBytes(int count)
        {
            return innerReader.ReadBytes(count);
        }

        public short ReadInt16()
        {
            return innerReader.ReadInt16();
        }

        public ushort ReadUInt16()
        {
            return innerReader.ReadUInt16();
        }

        public int ReadInt32()
        {
            return innerReader.ReadInt32();
        }

        public uint ReadUInt32()
        {
            return innerReader.ReadUInt32();
        }

        public long ReadInt64()
        {
            return innerReader.ReadInt64();
        }

        public ulong ReadUInt64()
        {
            return innerReader.ReadUInt64();
        }

        public void Offset(long byteCount)
        {
            innerReader.BaseStream.Seek(byteCount, SeekOrigin.Current);
        }

        #region IDisposable Support
        private bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed == false)
            {
                if (disposing)
                    innerReader.Dispose();

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
