using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils
{
    public static class NativeUtils
    {
        public static string GetNextString(byte[] buffer, ref int index, Encoding encoding)
        {
            int start = index;

            while (index < buffer.Length && buffer[index] != 0)
                index++;

            index++;

            return encoding.GetString(buffer, start, index - start - 1);
        }

        public static string GetFirstString(byte[] buffer, Encoding encoding)
        {
            int index = 0;
            return GetNextString(buffer, ref index, encoding);
        }
    }
}
