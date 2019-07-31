using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class WeaponsPackageProcessor : PackageProcessorBase
    {
        public readonly Dictionary<WeaponType, Dictionary<uint, WeaponPrimitiveBase>> Table = new Dictionary<WeaponType, Dictionary<uint, WeaponPrimitiveBase>>();

        public const ushort MeleeWeaponHeaderValue = 0x0186;
        public const ushort RangeWeaponHeaderValue = 0x01B1;

        public static bool IsWeaponInEquipmentPath(string chunkFullFilename)
        {
            if (chunkFullFilename.EndsWith(".wp_dat") == false && chunkFullFilename.EndsWith(".wp_dat_g") == false)
                return false;

            if (chunkFullFilename.StartsWith("/common/equip/") == false)
                return false;

            return true;
        }

        public static WeaponType GetWeaponTypeByFilename(string chunkFilename)
        {
            if (WeaponsUtils.WeaponFilenameToType.TryGetValue(chunkFilename, out WeaponType weaponType))
                return weaponType;

            return WeaponType.None;
        }

        public static WeaponType DetermineWeaponType(string chunkFullFilename)
        {
            string chunkFilename = Path.GetFileNameWithoutExtension(chunkFullFilename);

            return GetWeaponTypeByFilename(chunkFilename);
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            if (IsWeaponInEquipmentPath(chunkFullFilename) == false)
                return false;

            WeaponType weaponType = DetermineWeaponType(chunkFullFilename);

            return weaponType != WeaponType.None;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            WeaponType weaponType = DetermineWeaponType(chunkFullFilename);

            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                ushort headerValue = reader.ReadUInt16();
                uint numEntries = reader.ReadUInt32();

                if (headerValue == MeleeWeaponHeaderValue)
                    ProcessMeleeWeapons(reader, numEntries, weaponType);
                else if (headerValue == RangeWeaponHeaderValue)
                    ProcessRangeWeapons(reader, numEntries, weaponType);
                else
                    throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {MeleeWeaponHeaderValue:x4} (melee) or {RangeWeaponHeaderValue:x4} (range), read {headerValue:x4}.");
            }
        }

        private Dictionary<uint, WeaponPrimitiveBase> GetOrAddWeaponMap(WeaponType weaponType)
        {
            if (Table.TryGetValue(weaponType, out Dictionary<uint, WeaponPrimitiveBase> weapons) == false)
            {
                weapons = new Dictionary<uint, WeaponPrimitiveBase>();
                Table.Add(weaponType, weapons);
            }

            return weapons;
        }

        private void TryAddWeapon(WeaponPrimitiveBase weaponToAdd)
        {
            Dictionary<uint, WeaponPrimitiveBase> weapons = GetOrAddWeaponMap(weaponToAdd.WeaponType);

            if (weapons.ContainsKey(weaponToAdd.Id) == false)
                weapons.Add(weaponToAdd.Id, weaponToAdd);
        }

        private void ProcessMeleeWeapons(Reader reader, uint numEntries, WeaponType weaponType)
        {
            for (uint i = 0; i < numEntries; i++)
            {
                WeaponPrimitiveBase weapon = MeleeWeaponPrimitiveBase.Read(weaponType, reader);
                TryAddWeapon(weapon);
            }
        }

        private void ProcessRangeWeapons(Reader reader, uint numEntries, WeaponType weaponType)
        {
            for (uint i = 0; i < numEntries; i++)
            {
                WeaponPrimitiveBase weapon = RangeWeaponPrimitiveBase.Read(weaponType, reader);
                TryAddWeapon(weapon);
            }
        }
    }
}
