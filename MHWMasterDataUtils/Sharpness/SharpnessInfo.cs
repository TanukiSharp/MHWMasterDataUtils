using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Sharpness
{
    public struct SharpnessInfo
    {
        public readonly ushort Red;
        public readonly ushort Orange;
        public readonly ushort Yellow;
        public readonly ushort Green;
        public readonly ushort Blue;
        public readonly ushort White;
        public readonly ushort Purple;

        public SharpnessInfo(ushort red, ushort orange, ushort yellow, ushort green, ushort blue, ushort white, ushort purple)
        {
            Red = red;
            Orange = orange;
            Yellow = yellow;
            Green = green;
            Blue = blue;
            White = white;
            Purple = purple;
        }
    }
}
