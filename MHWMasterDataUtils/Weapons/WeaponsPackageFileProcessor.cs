using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class WeaponsPackageFileProcessor : PackageProcessorBase
    {
        private List<WeaponPrimitiveBase> weapons = new List<WeaponPrimitiveBase>();

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            if (chunkFullFilename.EndsWith(".wp_dat") == false && chunkFullFilename.EndsWith(".wp_dat_g") == false)
                return false;

            WeaponType weaponType = DetermineWeaponType(chunkFullFilename);

            return weaponType != WeaponType.None;
        }

        public const ushort MeleeWeaponHeaderIdentifier = 0x0186;
        public const ushort RangeWeaponHeaderIdentifier = 0x01B1;

        public WeaponType DetermineWeaponType(string chunkFullFilename)
        {
            switch (chunkFullFilename)
            {
                case @"\common\equip\l_sword.wp_dat": return WeaponType.GreatSword;
                case @"\common\equip\tachi.wp_dat": return WeaponType.LongSword;
                case @"\common\equip\sword.wp_dat": return WeaponType.SwordAndShield;
                case @"\common\equip\w_sword.wp_dat": return WeaponType.DualBlades;
                case @"\common\equip\hammer.wp_dat": return WeaponType.Hammer;
                case @"\common\equip\whistle.wp_dat": return WeaponType.HuntingHorn;
                case @"\common\equip\lance.wp_dat": return WeaponType.Lance;
                case @"\common\equip\g_lance.wp_dat": return WeaponType.Gunlance;
                case @"\common\equip\s_axe.wp_dat": return WeaponType.SwitchAxe;
                case @"\common\equip\c_axe.wp_dat": return WeaponType.ChargeBlade;
                case @"\common\equip\rod.wp_dat": return WeaponType.InsectGlaive;
                case @"\common\equip\lbg.wp_dat_g": return WeaponType.LightBowgun;
                case @"\common\equip\hbg.wp_dat_g": return WeaponType.HeavyBowgun;
                case @"\common\equip\bow.wp_dat_g": return WeaponType.Bow;
            }

            return WeaponType.None;
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            WeaponType weaponType = DetermineWeaponType(chunkFullFilename);

            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                ushort identifier = reader.ReadUInt16();

                if (identifier == MeleeWeaponHeaderIdentifier)
                    ProcessMeleeWeapons(reader, weaponType);
                else if (identifier == RangeWeaponHeaderIdentifier)
                    ProcessRangeWeapons(reader, weaponType);
                else
                    throw new FormatException($"Invalid header identifier in file '{reader.Filename ?? "<unknown>"}'. Expected {MeleeWeaponHeaderIdentifier:x4} or {RangeWeaponHeaderIdentifier:x4}, read {identifier:x4}.");
            }

            return Task.CompletedTask;
        }

        private void ProcessMeleeWeapons(Reader reader, WeaponType weaponType)
        {
            uint num_entries = reader.ReadUInt32();

            for (uint i = 0; i < num_entries; i++)
            {
                WeaponPrimitiveBase weapon = MeleeWeaponPrimitiveBase.Read(reader);
                weapon.weaponType = weaponType;
                weapons.Add(weapon);
            }
        }

        private void ProcessRangeWeapons(Reader reader, WeaponType weaponType)
        {
            uint num_entries = reader.ReadUInt32();

            for (uint i = 0; i < num_entries; i++)
            {
                WeaponPrimitiveBase weapon = RangeWeaponPrimitiveBase.Read(reader);
                weapon.weaponType = weaponType;
                weapons.Add(weapon);
            }
        }

        public override Task PostProcess()
        {
            weapons = weapons
                .OrderBy(x => x.weaponType)
                .ThenBy(x => x.id)
                .ToList();

            return base.PostProcess();
        }
    }
}
