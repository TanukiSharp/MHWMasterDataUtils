using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons
{
    public class InsectEntryPrimitive
    {
        public readonly ushort Id;
        public readonly InsectAttackType AttackType;
        public readonly byte Evolution;
        public readonly byte TreeId;
        public readonly ushort UpgradeCost;
        public readonly byte Rarity;
        public readonly ushort Power;
        public readonly ushort Speed;
        public readonly ushort Heal;
        public readonly InsectDustEffect DustEffect;
        public readonly ushort TreeOrder;

        private InsectEntryPrimitive(
            ushort id,
            InsectAttackType attackType,
            byte evolution,
            byte treeId,
            ushort upgradeCost,
            byte rarity,
            ushort power,
            ushort speed,
            ushort heal,
            InsectDustEffect dustEffect,
            ushort treeOrder
        )
        {
            Id = id;
            AttackType = attackType;
            Evolution = evolution;
            TreeId = treeId;
            UpgradeCost = upgradeCost;
            Rarity = rarity;
            Power = power;
            Speed = speed;
            Heal = heal;
            DustEffect = dustEffect;
            TreeOrder = treeOrder;
        }

        public static InsectEntryPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown1 and unknown2.
            var attackType = (InsectAttackType)reader.ReadByte();
            reader.Offset(1); // Skip Order.
            byte evolution = reader.ReadByte();
            reader.Offset(2); // Skip modelId.
            byte treeId = reader.ReadByte();
            ushort upgradeCost = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown3.
            byte rarity = reader.ReadByte();
            ushort power = reader.ReadUInt16();
            ushort speed = reader.ReadUInt16();
            ushort heal = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown4.
            var dustEffect = (InsectDustEffect)reader.ReadUInt16();
            reader.Offset(2); // Skip treePosition.
            ushort treeOrder = reader.ReadUInt16();

            return new InsectEntryPrimitive(
                id,
                attackType,
                evolution,
                treeId,
                upgradeCost,
                rarity,
                power,
                speed,
                heal,
                dustEffect,
                treeOrder
            );
        }
    }
}
