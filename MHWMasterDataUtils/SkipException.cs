using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils
{
    public class SkipException : Exception
    {
        public SkipException()
        {
        }

        public SkipException(string message)
            : base(message)
        {
        }

        public SkipException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
