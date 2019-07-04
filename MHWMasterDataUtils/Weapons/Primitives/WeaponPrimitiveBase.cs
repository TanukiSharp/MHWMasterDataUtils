using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipments;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class WeaponPrimitiveBase : IComparable<WeaponPrimitiveBase>
    {
        public WeaponClass WeaponClass { get; }
        public uint Id { get; }
        public ushort BaseModelId { get; }
        public ushort Part1Id { get; }
        public ushort Part2Id { get; }
        public byte Color { get; }
        public byte TreeId { get; }
        public FixedUpgradePrimitive IsFixedUpgrade { get; }
        public uint CraftingCost { get; }
        public byte Rarity { get; }
        public ushort RawDamage { get; } // raw damage, display value depends on weapon display multiplier
        public ushort Defense { get; }
        public sbyte Affinity { get; }
        public ElementStatus ElementId { get; } // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
        public ushort ElementDamage { get; } // appears multiplied by 10 in game
        public ElementStatus HiddenElement_id { get; } // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
        public ushort HiddenElementDamage { get; } // appears multiplied by 10 in game
        public EldersealPrimitive Elderseal { get; } // 0: none, 1: low, 2: average, 3: high
        public byte GemSlots { get; }
        public byte GemSlot1 { get; }
        public byte GemSlot2 { get; }
        public byte GemSlot3 { get; }
        public byte TreePosition { get; }
        public ushort Order { get; }
        public ushort GmdNameIndex { get; }
        public ushort GmdDescriptionIndex { get; }
        public ushort SkillId { get; }

        public WeaponPrimitiveBase(
            WeaponClass weaponClass,
            uint id,
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
            ElementStatus hiddenElement_id,
            ushort hiddenElementDamage,
            EldersealPrimitive elderseal,
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
            WeaponClass = weaponClass;
            Id = id;
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
            HiddenElement_id = hiddenElement_id;
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

            if (other.WeaponClass != WeaponClass)
                diff++;
            if (other.Id != Id)
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
            if (other.HiddenElement_id != HiddenElement_id)
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
