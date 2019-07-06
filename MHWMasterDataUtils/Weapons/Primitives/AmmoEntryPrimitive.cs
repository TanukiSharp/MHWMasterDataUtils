using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public struct AmmoEntryPrimitive
    {
        public readonly byte MagazineCount; // max: 10
        public readonly AmmoRecoilPrimitive RecoilAmount; // rapid: 1c, 1d, 1e, 1f, 21
        public readonly AmmoReloadPrimitive ReloadSpeed; // normal: 00/01/0e/12

        private AmmoEntryPrimitive(byte magazineCount, AmmoRecoilPrimitive recoilAmount, AmmoReloadPrimitive reloadSpeed)
        {
            MagazineCount = magazineCount;
            RecoilAmount = recoilAmount;
            ReloadSpeed = reloadSpeed;
        }

        public static AmmoEntryPrimitive Read(Reader reader)
        {
            byte magazineCount = reader.ReadByte();
            AmmoRecoilPrimitive recoilAmount = FromRecoilValue(reader.ReadByte());
            AmmoReloadPrimitive reloadSpeed = FromReloadValue(reader.ReadByte());

            return new AmmoEntryPrimitive(magazineCount, recoilAmount, reloadSpeed);
        }

        private static AmmoRecoilPrimitive FromRecoilValue(byte recoilAmount)
        {
            switch (recoilAmount)
            {
                case 0x0:
                    return AmmoRecoilPrimitive.NormalRecoil1;

                case 0x1:
                case 0x2:
                case 0x3:
                    return AmmoRecoilPrimitive.NormalRecoil2;

                case 0x4:
                case 0x5:
                case 0x7:
                case 0xB:
                case 0x14:
                case 0x15:
                case 0x18:
                case 0x20:
                    return AmmoRecoilPrimitive.NormalRecoil3;

                case 0x6:
                case 0x8:
                case 0x9:
                case 0xC:
                case 0xD:
                case 0x13:
                case 0x19:
                    return AmmoRecoilPrimitive.NormalRecoil4;

                case 0x1C:
                case 0x1D:
                case 0x1E:
                    return AmmoRecoilPrimitive.RapidFireRecoil2;

                case 0x1F:
                case 0x21:
                    return AmmoRecoilPrimitive.RapidFireRecoil3;

                case 0x12:
                    return AmmoRecoilPrimitive.FollowUpMortarRecoil1;

                case 0xE:
                case 0x1B:
                    return AmmoRecoilPrimitive.FollowUpMortarRecoil2;

                case 0xF:
                case 0x10:
                case 0x16:
                case 0x17:
                case 0x1A:
                    return AmmoRecoilPrimitive.FollowUpMortarRecoil3;

                case 0xA:
                    return AmmoRecoilPrimitive.BlotActionSingleShotAutoReload;

                case 0x11:
                    return AmmoRecoilPrimitive.WyvernShotCharge;
            }

            throw new FormatException($"Unknown ammo recoil value '{recoilAmount}'");
        }

        private static AmmoReloadPrimitive FromReloadValue(byte reloadAmount)
        {
            switch (reloadAmount)
            {
                case 0x11:
                    return AmmoReloadPrimitive.Fast;

                case 0x00:
                case 0x01:
                case 0x0E:
                case 0x12:
                    return AmmoReloadPrimitive.Normal;

                case 0x02:
                case 0x03:
                case 0x04:
                case 0x05:
                case 0x0B:
                case 0x0F:
                case 0x10:
                    return AmmoReloadPrimitive.Slow;

                case 0x06:
                case 0x07:
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0C:
                case 0x0D:
                    return AmmoReloadPrimitive.VerySlow;
            }

            throw new FormatException($"Unknown ammo reload value '{reloadAmount}'");
        }
    }
}
