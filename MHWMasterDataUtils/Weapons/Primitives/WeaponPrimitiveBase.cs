using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class WeaponPrimitiveBase : IComparable<WeaponPrimitiveBase>
    {
        public readonly WeaponType WeaponType;
        public readonly uint Id;
        public readonly ushort TreeOrder;
        public readonly ushort BaseModelId;
        public readonly ushort Part1Id;
        public readonly ushort Part2Id;
        public readonly byte Color;
        public readonly byte TreeId;
        public readonly FixedUpgradePrimitive IsFixedUpgrade;
        public readonly uint CraftingCost;
        public readonly byte Rarity;
        public readonly ushort RawDamage; // raw damage, display value depends on weapon display multiplier
        public readonly ushort Defense;
        public readonly sbyte Affinity;
        public readonly ElementStatus ElementId; // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
        public readonly ushort ElementDamage; // appears multiplied by 10 in game
        public readonly ElementStatus HiddenElementId; // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
        public readonly ushort HiddenElementDamage; // appears multiplied by 10 in game
        public readonly Elderseal Elderseal; // 0: none, 1: low, 2: average, 3: high
        public readonly byte GemSlots;
        public readonly byte GemSlot1;
        public readonly byte GemSlot2;
        public readonly byte GemSlot3;
        public readonly byte TreePosition;
        public readonly ushort Order;
        public readonly ushort GmdNameIndex;
        public readonly ushort GmdDescriptionIndex;
        public readonly ushort SkillId;

        protected WeaponPrimitiveBase(
            WeaponType weaponType,
            uint id,
            ushort treeOrder,
            ushort baseModelId,
            ushort part1Id,
            ushort part2Id,
            byte color,
            byte treeId,
            FixedUpgradePrimitive isFixedUpgrade,
            uint craftingCost,
            byte rarity,
            ushort rawDamage,
            ushort defense,
            sbyte affinity,
            ElementStatus elementId,
            ushort elementDamage,
            ElementStatus hiddenElementId,
            ushort hiddenElementDamage,
            Elderseal elderseal,
            byte gemSlots,
            byte gemSlot1,
            byte gemSlot2,
            byte gemSlot3,
            byte treePosition,
            ushort order,
            ushort gmdNameIndex,
            ushort gmdDescriptionIndex,
            ushort skillId
        )
        {
            WeaponType = weaponType;
            Id = id;
            TreeOrder = treeOrder;
            BaseModelId = baseModelId;
            Part1Id = part1Id;
            Part2Id = part2Id;
            Color = color;
            TreeId = treeId;
            IsFixedUpgrade = isFixedUpgrade;
            CraftingCost = craftingCost;
            Rarity = rarity;
            RawDamage = rawDamage;
            Defense = defense;
            Affinity = affinity;
            ElementId = elementId;
            ElementDamage = elementDamage;
            HiddenElementId = hiddenElementId;
            HiddenElementDamage = hiddenElementDamage;
            Elderseal = elderseal;
            GemSlots = gemSlots;
            GemSlot1 = gemSlot1;
            GemSlot2 = gemSlot2;
            GemSlot3 = gemSlot3;
            TreePosition = treePosition;
            Order = order;
            GmdNameIndex = gmdNameIndex;
            GmdDescriptionIndex = gmdDescriptionIndex;
            SkillId = skillId;
        }

        public virtual int CompareTo(WeaponPrimitiveBase other)
        {
            int diff = 0;

            if (other.WeaponType != WeaponType)
                diff++;
            if (other.Id != Id)
                diff++;
            if (other.TreeOrder != TreeOrder)
                diff++;
            if (other.BaseModelId != BaseModelId)
                diff++;
            if (other.Part1Id != Part1Id)
                diff++;
            if (other.Part2Id != Part2Id)
                diff++;
            if (other.Color != Color)
                diff++;
            if (other.TreeId != TreeId)
                diff++;
            if (other.IsFixedUpgrade != IsFixedUpgrade)
                diff++;
            if (other.CraftingCost != CraftingCost)
                diff++;
            if (other.Rarity != Rarity)
                diff++;
            if (other.RawDamage != RawDamage)
                diff++;
            if (other.Defense != Defense)
                diff++;
            if (other.Affinity != Affinity)
                diff++;
            if (other.ElementId != ElementId)
                diff++;
            if (other.ElementDamage != ElementDamage)
                diff++;
            if (other.HiddenElementId != HiddenElementId)
                diff++;
            if (other.HiddenElementDamage != HiddenElementDamage)
                diff++;
            if (other.Elderseal != Elderseal)
                diff++;
            if (other.GemSlots != GemSlots)
                diff++;
            if (other.GemSlot1 != GemSlot1)
                diff++;
            if (other.GemSlot2 != GemSlot2)
                diff++;
            if (other.GemSlot3 != GemSlot3)
                diff++;
            if (other.TreePosition != TreePosition)
                diff++;
            if (other.Order != Order)
                diff++;
            if (other.GmdNameIndex != GmdNameIndex)
                diff++;
            if (other.GmdDescriptionIndex != GmdDescriptionIndex)
                diff++;
            if (other.SkillId != SkillId)
                diff++;

            return diff;
        }
    }
}
