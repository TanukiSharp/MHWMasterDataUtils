using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Sharpness
{
    public static class SharpnessUtils
    {
        private static readonly ushort[] sharpnessValues = new ushort[7];

        public static ushort ToSharpnessModifier(byte handicraft)
        {
            switch (handicraft)
            {
                case 5: return 0;
                case 4: return 50;
                case 3: return 100;
                case 2: return 150;
                case 1: return 200;
                case 0: return 250;
            }

            throw new FormatException($"Invalid '{nameof(handicraft)}' argument (value='{handicraft}').");
        }

        public static SharpnessInfo ApplySharpnessModifier(ushort modifier, SharpnessInfo sharpness)
        {
            sharpness.ToArray(sharpnessValues);

            for (int i = sharpnessValues.Length - 1; i >= 0; i--)
            {
                if (sharpnessValues[i] == 0)
                    continue;

                if (modifier > sharpnessValues[i])
                {
                    modifier -= sharpnessValues[i];
                    sharpnessValues[i] = 0;
                }
                else
                {
                    sharpnessValues[i] -= modifier;
                    break;
                }
            }

            return new SharpnessInfo(
                sharpnessValues[0],
                sharpnessValues[1],
                sharpnessValues[2],
                sharpnessValues[3],
                sharpnessValues[4],
                sharpnessValues[5],
                sharpnessValues[6]
            );
        }
    }
}
