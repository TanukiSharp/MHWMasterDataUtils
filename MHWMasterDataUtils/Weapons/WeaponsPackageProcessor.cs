using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class WeaponsPackageProcessor : PackageProcessorBase
    {
        private List<WeaponPrimitiveBase> weapons = new List<WeaponPrimitiveBase>();

        public const ushort MeleeWeaponHeaderValue = 0x0186;
        public const ushort RangeWeaponHeaderValue = 0x01B1;

        public static bool IsWeaponInEquipmentPath(string chunkFullFilename)
        {
            if (chunkFullFilename.EndsWith(".wp_dat") == false && chunkFullFilename.EndsWith(".wp_dat_g") == false)
                return false;

            if (chunkFullFilename.StartsWith(@"\common\equip\") == false)
                return false;

            return true;
        }

        public static WeaponClass GetWeaponClassByFilename(string chunkFilename)
        {
            if (WeaponFilenameUtils.WeaponFilenameToClass.TryGetValue(chunkFilename, out WeaponClass weaponClass))
                return weaponClass;

            return WeaponClass.None;
        }

        public static WeaponClass DetermineWeaponClass(string chunkFullFilename)
        {
            string chunkFilename = Path.GetFileNameWithoutExtension(chunkFullFilename);

            return GetWeaponClassByFilename(chunkFilename);
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            if (IsWeaponInEquipmentPath(chunkFullFilename) == false)
                return false;

            WeaponClass weaponType = DetermineWeaponClass(chunkFullFilename);

            return weaponType != WeaponClass.None;
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            WeaponClass weaponType = DetermineWeaponClass(chunkFullFilename);

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

            return Task.CompletedTask;
        }

        public WeaponPrimitiveBase FindWeapon(WeaponClass weaponClass, uint id)
        {
            foreach (WeaponPrimitiveBase weapon in weapons)
            {
                if (weapon.weaponClass == weaponClass && weapon.id == id)
                    return weapon;
            }

            return null;
        }

        private void AddWeapon(WeaponPrimitiveBase weaponToAdd)
        {
            if (FindWeapon(weaponToAdd.weaponClass, weaponToAdd.id) != null)
                return;

            weapons.Add(weaponToAdd);
        }

        private void ProcessMeleeWeapons(Reader reader, uint numEntries, WeaponClass weaponClass)
        {
            for (uint i = 0; i < numEntries; i++)
            {
                WeaponPrimitiveBase weapon = MeleeWeaponPrimitiveBase.Read(reader);
                weapon.weaponClass = weaponClass;
                AddWeapon(weapon);
            }
        }

        private void ProcessRangeWeapons(Reader reader, uint numEntries, WeaponClass weaponClass)
        {
            for (uint i = 0; i < numEntries; i++)
            {
                WeaponPrimitiveBase weapon = RangeWeaponPrimitiveBase.Read(reader);
                weapon.weaponClass = weaponClass;
                AddWeapon(weapon);
            }
        }

        public override Task PostProcess()
        {
            weapons = weapons
                .OrderBy(x => x.weaponClass)
                .ThenBy(x => x.id)
                .ToList();

            return base.PostProcess();
        }
    }
}
