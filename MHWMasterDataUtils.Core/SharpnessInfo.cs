using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Core
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

        public static SharpnessInfo FromAbsoluteValues(ushort red, ushort orange, ushort yellow, ushort green, ushort blue, ushort white, ushort purple)
        {
            return new SharpnessInfo(
                red,
                (ushort)(orange - red),
                (ushort)(yellow - orange),
                (ushort)(green - yellow),
                (ushort)(blue - green),
                (ushort)(white - blue),
                purple > 0 ? (ushort)(purple - white) : (ushort)0
            );
        }

        public ushort[] ToArray()
        {
            return new ushort[]
            {
                Red,
                Orange,
                Yellow,
                Green,
                Blue,
                White,
                Purple
            };
        }

        public void ToArray(ushort[] output)
        {
            if (output.Length < 7)
                throw new ArgumentOutOfRangeException(nameof(output), $"Argument '{nameof(output)}' must be of length 7 or more.");

            output[0] = Red;
            output[1] = Orange;
            output[2] = Yellow;
            output[3] = Green;
            output[4] = Blue;
            output[5] = White;
            output[6] = Purple;
        }

        public override string ToString()
        {
            return $"{Red}, {Orange}, {Yellow}, {Green}, {Blue}, {White}, {Purple}";
        }
    }
}
