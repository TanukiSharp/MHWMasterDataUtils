using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Weapons.HighLevel;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public static class WeaponsUtils
    {
        public static Dictionary<string, WeaponType> WeaponFilenameToType { get; } = new Dictionary<string, WeaponType>
        {
            ["l_sword"] = WeaponType.GreatSword,
            ["tachi"] = WeaponType.LongSword,
            ["sword"] = WeaponType.SwordAndShield,
            ["w_sword"] = WeaponType.DualBlades,
            ["hammer"] = WeaponType.Hammer,
            ["whistle"] = WeaponType.HuntingHorn,
            ["lance"] = WeaponType.Lance,
            ["g_lance"] = WeaponType.Gunlance,
            ["s_axe"] = WeaponType.SwitchAxe,
            ["c_axe"] = WeaponType.ChargeBlade,
            ["rod"] = WeaponType.InsectGlaive,
            ["lbg"] = WeaponType.LightBowgun,
            ["hbg"] = WeaponType.HeavyBowgun,
            ["bow"] = WeaponType.Bow
        };

        public static Dictionary<WeaponType, string> WeaponTypeToFilename { get; } = new Dictionary<WeaponType, string>
        {
            [WeaponType.GreatSword] = "l_sword",
            [WeaponType.LongSword] = "tachi",
            [WeaponType.SwordAndShield] = "sword",
            [WeaponType.DualBlades] = "w_sword",
            [WeaponType.Hammer] = "hammer",
            [WeaponType.HuntingHorn] = "whistle",
            [WeaponType.Lance] = "lance",
            [WeaponType.Gunlance] = "g_lance",
            [WeaponType.SwitchAxe] = "s_axe",
            [WeaponType.ChargeBlade] = "c_axe",
            [WeaponType.InsectGlaive] = "rod",
            [WeaponType.LightBowgun] = "lbg",
            [WeaponType.HeavyBowgun] = "hbg",
            [WeaponType.Bow] = "bow"
        };

        public static ushort ComputeWeaponDamage(WeaponType weaponType, ushort rawDamage)
        {
            switch (weaponType)
            {
                case WeaponType.GreatSword: return (ushort)(rawDamage * RawDamageMultipliers.GreatSword);
                case WeaponType.SwordAndShield: return (ushort)(rawDamage * RawDamageMultipliers.SwordAndShield);
                case WeaponType.DualBlades: return (ushort)(rawDamage * RawDamageMultipliers.DualBlades);
                case WeaponType.LongSword: return (ushort)(rawDamage * RawDamageMultipliers.LongSword);
                case WeaponType.Hammer: return (ushort)(rawDamage * RawDamageMultipliers.Hammer);
                case WeaponType.HuntingHorn: return (ushort)(rawDamage * RawDamageMultipliers.HuntingHorn);
                case WeaponType.Lance: return (ushort)(rawDamage * RawDamageMultipliers.Lance);
                case WeaponType.Gunlance: return (ushort)(rawDamage * RawDamageMultipliers.Gunlance);
                case WeaponType.SwitchAxe: return (ushort)(rawDamage * RawDamageMultipliers.SwitchAxe);
                case WeaponType.ChargeBlade: return (ushort)(rawDamage * RawDamageMultipliers.ChargeBlade);
                case WeaponType.InsectGlaive: return (ushort)(rawDamage * RawDamageMultipliers.InsectGlaive);
                case WeaponType.Bow: return (ushort)(rawDamage * RawDamageMultipliers.Bow);
                case WeaponType.HeavyBowgun: return (ushort)(rawDamage * RawDamageMultipliers.HeavyBowgun);
                case WeaponType.LightBowgun: return (ushort)(rawDamage * RawDamageMultipliers.LightBowgun);
            }

            throw new ArgumentException($"Invalid '{nameof(weaponType)}' argument. Unknown value '{weaponType}'.");
        }

        public static ushort[] CreateSlotsArray(WeaponPrimitiveBase weapon)
        {
            ushort[] slots = new ushort[weapon.GemSlots];

            if (slots.Length > 0)
            {
                slots[0] = weapon.GemSlot1;
                if (slots.Length > 1)
                {
                    slots[1] = weapon.GemSlot2;
                    if (slots.Length > 2)
                        slots[2] = weapon.GemSlot3;
                }
            }

            return slots;
        }
    }
}
