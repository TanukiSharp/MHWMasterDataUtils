using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Weapons.HighLevel;

namespace MHWMasterDataUtils.Weapons
{
    public static class WeaponsUtils
    {
        public static Dictionary<string, WeaponClass> WeaponFilenameToClass { get; } = new Dictionary<string, WeaponClass>
        {
            ["l_sword"] = WeaponClass.GreatSword,
            ["tachi"] = WeaponClass.LongSword,
            ["sword"] = WeaponClass.SwordAndShield,
            ["w_sword"] = WeaponClass.DualBlades,
            ["hammer"] = WeaponClass.Hammer,
            ["whistle"] = WeaponClass.HuntingHorn,
            ["lance"] = WeaponClass.Lance,
            ["g_lance"] = WeaponClass.Gunlance,
            ["s_axe"] = WeaponClass.SwitchAxe,
            ["c_axe"] = WeaponClass.ChargeBlade,
            ["rod"] = WeaponClass.InsectGlaive,
            ["lbg"] = WeaponClass.LightBowgun,
            ["hbg"] = WeaponClass.HeavyBowgun,
            ["bow"] = WeaponClass.Bow
        };

        public static Dictionary<WeaponClass, string> WeaponClassToFilename { get; } = new Dictionary<WeaponClass, string>
        {
            [WeaponClass.GreatSword] = "l_sword",
            [WeaponClass.LongSword] = "tachi",
            [WeaponClass.SwordAndShield] = "sword",
            [WeaponClass.DualBlades] = "w_sword",
            [WeaponClass.Hammer] = "hammer",
            [WeaponClass.HuntingHorn] = "whistle",
            [WeaponClass.Lance] = "lance",
            [WeaponClass.Gunlance] = "g_lance",
            [WeaponClass.SwitchAxe] = "s_axe",
            [WeaponClass.ChargeBlade] = "c_axe",
            [WeaponClass.InsectGlaive] = "rod",
            [WeaponClass.LightBowgun] = "lbg",
            [WeaponClass.HeavyBowgun] = "hbg",
            [WeaponClass.Bow] = "bow"
        };

        public static WeaponType FromWeaponClass(WeaponClass weaponClass)
        {
            switch (weaponClass)
            {
                case WeaponClass.GreatSword: return WeaponType.GreatSword;
                case WeaponClass.SwordAndShield: return WeaponType.SwordAndShield;
                case WeaponClass.DualBlades: return WeaponType.DualBlades;
                case WeaponClass.LongSword: return WeaponType.LongSword;
                case WeaponClass.Hammer: return WeaponType.Hammer;
                case WeaponClass.HuntingHorn: return WeaponType.HuntingHorn;
                case WeaponClass.Lance: return WeaponType.Lance;
                case WeaponClass.Gunlance: return WeaponType.Gunlance;
                case WeaponClass.SwitchAxe: return WeaponType.SwitchAxe;
                case WeaponClass.ChargeBlade: return WeaponType.ChargeBlade;
                case WeaponClass.InsectGlaive: return WeaponType.InsectGlaive;
                case WeaponClass.Bow: return WeaponType.Bow;
                case WeaponClass.HeavyBowgun: return WeaponType.HeavyBowgun;
                case WeaponClass.LightBowgun: return WeaponType.LightBowgun;
            }

            throw new ArgumentException($"Invalid '{nameof(weaponClass)}' argument. Unknown value '{weaponClass}'.");
        }
    }
}
