//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace MHWMasterDataUtils.Weapons.HighLevel
//{
//    public class WeaponBase
//    {
//        public WeaponType WeaponType { get; }
//        public uint Id { get; }
//        public byte Color { get; }
//        public byte TreeId;
//        public FixedUpgradePrimitive IsFixedUpgrade;
//        public uint CraftingCost;
//        public byte Rarity;
//        public ushort RawDamage; // raw damage, display value depends on weapon display multiplier
//        public ushort Defense;
//        public sbyte Affinity;
//        public ElementStatus ElementId; // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
//        public ushort ElementDamage; // appears multiplied by 10 in game
//        public ElementStatus HiddenElementId; // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
//        public ushort HiddenElementDamage; // appears multiplied by 10 in game
//        public EldersealPrimitive Elderseal; // 0: none, 1: low, 2: average, 3: high
//        public byte GemSlots;
//        public byte GemSlot1;
//        public byte GemSlot2;
//        public byte GemSlot3;
//        public byte TreePosition;
//        public ushort Order;
//        public ushort GmdNameIndex;
//        public ushort GmdDescriptionIndex;
//        public ushort SkillId;

//        protected WeaponPrimitiveBase(
//            WeaponClass weaponClass,
//            uint id,
//            ushort baseModelId,
//            ushort part1Id,
//            ushort part2Id,
//            byte color,
//            byte treeId,
//            FixedUpgradePrimitive isFixedUpgrade,
//            uint craftingCost,
//            byte rarity,
//            ushort rawDamage,
//            ushort defense,
//            sbyte affinity,
//            ElementStatus elementId,
//            ushort elementDamage,
//            ElementStatus hiddenElementId,
//            ushort hiddenElementDamage,
//            EldersealPrimitive elderseal,
//            byte gemSlots,
//            byte gemSlot1,
//            byte gemSlot2,
//            byte gemSlot3,
//            byte treePosition,
//            ushort order,
//            ushort gmdNameIndex,
//            ushort gmdDescriptionIndex,
//            ushort skillId
//        )
//        {
//            WeaponClass = weaponClass;
//            Id = id;
//            BaseModelId = baseModelId;
//            Part1Id = part1Id;
//            Part2Id = part2Id;
//            Color = color;
//            TreeId = treeId;
//            IsFixedUpgrade = isFixedUpgrade;
//            CraftingCost = craftingCost;
//            Rarity = rarity;
//            RawDamage = rawDamage;
//            Defense = defense;
//            Affinity = affinity;
//            ElementId = elementId;
//            ElementDamage = elementDamage;
//            HiddenElementId = hiddenElementId;
//            HiddenElementDamage = hiddenElementDamage;
//            Elderseal = elderseal;
//            GemSlots = gemSlots;
//            GemSlot1 = gemSlot1;
//            GemSlot2 = gemSlot2;
//            GemSlot3 = gemSlot3;
//            TreePosition = treePosition;
//            Order = order;
//            GmdNameIndex = gmdNameIndex;
//            GmdDescriptionIndex = gmdDescriptionIndex;
//            SkillId = skillId;
//        }
//    }
//}
