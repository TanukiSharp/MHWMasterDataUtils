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

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            WeaponClass weaponType = DetermineWeaponClass(chunkFullFilename, true);

            return weaponType != WeaponClass.None;
        }

        public const ushort MeleeWeaponHeaderIdentifier = 0x0186;
        public const ushort RangeWeaponHeaderIdentifier = 0x01B1;

        public WeaponClass DetermineWeaponClass(string chunkFullFilename, bool fullCheck)
        {
            if (fullCheck)
            {
                if (chunkFullFilename.StartsWith(@"\common\equip\") == false)
                    return WeaponClass.None;

                if (chunkFullFilename.EndsWith(".wp_dat") == false && chunkFullFilename.EndsWith(".wp_dat_g") == false)
                    return WeaponClass.None;
            }

            string filename = Path.GetFileNameWithoutExtension(chunkFullFilename);

            if (WeaponFilenameUtils.WeaponFilenameToClass.TryGetValue(filename, out WeaponClass weaponClass))
                return weaponClass;

            return WeaponClass.None;
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            WeaponClass weaponType = DetermineWeaponClass(chunkFullFilename, false);

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

        private void ProcessMeleeWeapons(Reader reader, WeaponClass weaponClass)
        {
            uint num_entries = reader.ReadUInt32();

            for (uint i = 0; i < num_entries; i++)
            {
                WeaponPrimitiveBase weapon = MeleeWeaponPrimitiveBase.Read(reader);
                weapon.weaponClass = weaponClass;
                AddWeapon(weapon);
            }
        }

        private void ProcessRangeWeapons(Reader reader, WeaponClass weaponClass)
        {
            uint num_entries = reader.ReadUInt32();

            for (uint i = 0; i < num_entries; i++)
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
