using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons
{
    public struct AmmoEntryPrimitive
    {
        public readonly byte Capacity; // max: 10
        public readonly AmmoShotType ShotType; // rapid: 1c, 1d, 1e, 1f, 21
        public readonly AmmoReload Reload; // normal: 00/01/0e/12

        //public readonly string ShotTypeBinary;
        //public readonly string ReloadBinary;

        private AmmoEntryPrimitive(byte capacity, AmmoShotType shotType, AmmoReload reload/*, string shotTypeBinary, string reloadBinary*/)
        {
            Capacity = capacity;
            ShotType = shotType;
            Reload = reload;

            //ShotTypeBinary = shotTypeBinary;
            //ReloadBinary = reloadBinary;
        }

        public static AmmoEntryPrimitive Read(Reader reader)
        {
            byte capacity = reader.ReadByte();
            byte shotTypeNum = reader.ReadByte();
            byte reloadNum = reader.ReadByte();

            AmmoShotType shotType = FromShotTypeValue(shotTypeNum);
            AmmoReload reload = FromReloadValue(reloadNum);

            //string shotTypeBinary = Convert.ToString(shotTypeNum, 2).PadLeft(8, '0').Insert(4, "_");
            //string reloadBinary = Convert.ToString(reloadNum, 2).PadLeft(8, '0').Insert(4, "_");

            return new AmmoEntryPrimitive(capacity, shotType, reload/*, shotTypeBinary, reloadBinary*/);
        }

        private static AmmoShotType FromShotTypeValue(byte shotType)
        {
            switch (shotType)
            {
                case 0b_0000_0000:
                    return AmmoShotType.NormalRecoil1;

                case 0b_0000_0001:
                case 0b_0000_0010:
                case 0b_0000_0011:
                    return AmmoShotType.NormalRecoil2;

                case 0b_0000_0100:
                case 0b_0000_0101:
                case 0b_0000_0111:
                case 0b_0000_1011:
                case 0b_0001_0100:
                case 0b_0001_0101:
                case 0b_0001_1000:
                case 0b_0010_0000:
                    return AmmoShotType.NormalRecoil3;

                case 0b_0000_0110:
                case 0b_0000_1000:
                case 0b_0000_1001:
                case 0b_0000_1100:
                case 0b_0000_1101:
                case 0b_0001_0011:
                case 0b_0001_1001:
                    return AmmoShotType.NormalRecoil4;

                case 0b_0001_1100:
                case 0b_0001_1101:
                case 0b_0001_1110:
                    return AmmoShotType.RapidFireRecoil2;

                case 0b_0001_1111:
                case 0b_0010_0001:
                    return AmmoShotType.RapidFireRecoil3;

                case 0b_0001_0010:
                    return AmmoShotType.FollowUpRecoil1;

                case 0b_0000_1110:
                case 0b_0001_1011:
                    return AmmoShotType.FollowUpRecoil2;

                case 0b_0000_1111:
                case 0b_0001_0000:
                case 0b_0001_0110:
                case 0b_0001_0111:
                case 0b_0001_1010:
                    return AmmoShotType.FollowUpRecoil3;

                case 0b_0000_1010:
                    return AmmoShotType.NormalAutoReload;

                case 0b_0001_0001:
                    return AmmoShotType.Wyvern;
            }

            throw new FormatException($"Unknown ammo shot type value '{shotType}'");
        }

        private static AmmoReload FromReloadValue(byte reloadAmount)
        {
            switch (reloadAmount)
            {
                case 0b_0001_0001:
                    return AmmoReload.Fast;

                case 0b_0000_0000:
                case 0b_0000_0001:
                case 0b_0000_1110:
                case 0b_0001_0010:
                    return AmmoReload.Normal;

                case 0b_0000_0010:
                case 0b_0000_0011:
                case 0b_0000_0100:
                case 0b_0000_0101:
                case 0b_0000_1011:
                case 0b_0000_1111:
                case 0b_0001_0000:
                    return AmmoReload.Slow;

                case 0b_0000_0110:
                case 0b_0000_0111:
                case 0b_0000_1000:
                case 0b_0000_1001:
                case 0b_0000_1010:
                case 0b_0000_1100:
                case 0b_0000_1101:
                    return AmmoReload.VerySlow;
            }

            throw new FormatException($"Unknown ammo reload value '{reloadAmount}'");
        }

        public override string ToString()
        {
            return $"{Capacity}, shotType: {ShotType}, reload: {Reload}";
        }
    }
}
