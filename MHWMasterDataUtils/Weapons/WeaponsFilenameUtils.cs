using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipments;

namespace MHWMasterDataUtils.Weapons
{
    public static class WeaponsFilenameUtils
    {
        public static readonly Dictionary<string, WeaponClass> WeaponFilenameToClass = new Dictionary<string, WeaponClass>
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

        public static readonly Dictionary<WeaponClass, string> WeaponClassToFilename = new Dictionary<WeaponClass, string>
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
    }
}
