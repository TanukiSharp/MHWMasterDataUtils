using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public struct AmmoEntryPrimitive
    {
        public readonly byte MagazineCount; // max: 10
        public readonly byte RecoilAmount; // rapid: 1c, 1d, 1e, 1f, 21
        public readonly byte ReloadSpeed; // normal: 00/01/0e/12

        private AmmoEntryPrimitive(byte magazineCount, byte recoilAmount, byte reloadSpeed)
        {
            MagazineCount = magazineCount;
            RecoilAmount = recoilAmount;
            ReloadSpeed = reloadSpeed;
        }

        public static AmmoEntryPrimitive Read(Reader reader)
        {
            byte magazineCount = reader.ReadByte();
            byte recoilAmount = reader.ReadByte();
            byte reloadSpeed = reader.ReadByte();

            return new AmmoEntryPrimitive(magazineCount, recoilAmount, reloadSpeed);
        }
    }
}
