using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public class CharmEquipmentBuilder : EquipmentBuilderBase<Charm>
    {
        private readonly Dictionary<ushort, EquipmentUpgradeEntryPrimitive> equipmentUpgrades;

        public CharmEquipmentBuilder(
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            EquipmentCraftPackageProcessor<EquipmentType> equipmentCraft,
            EquipmentUpgradePackageProcessor equipmentUpgrades
        )
            : base(
                  EquipmentType.Charm,
                  x => x.EquipSlot == EquipmentTypePrimitive.Charm,
                  equipments,
                  equipmentLanguages,
                  equipmentCraft
            )
        {
            this.equipmentUpgrades = equipmentUpgrades.Table[(byte)EquipmentType.Charm];
        }

        protected override CraftItem[] CreateCraft(EquipmentPrimitive equipment)
        {
            CraftItem[] craft = base.CreateCraft(equipment);

            if (craft != null)
                return craft;

            if (equipmentUpgrades.TryGetValue(equipment.Id, out EquipmentUpgradeEntryPrimitive upgradeEntry) == false)
                return null;

            var result = new List<CraftItem>();

            TryAddCraft(result, upgradeEntry.Item1Id, upgradeEntry.Item1Quantity);
            TryAddCraft(result, upgradeEntry.Item2Id, upgradeEntry.Item2Quantity);
            TryAddCraft(result, upgradeEntry.Item3Id, upgradeEntry.Item3Quantity);
            TryAddCraft(result, upgradeEntry.Item4Id, upgradeEntry.Item4Quantity);

            return result.ToArray();
        }
    }
}
