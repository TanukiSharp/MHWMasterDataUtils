THIS IS STILL WORK IN PROGRESS

# Overview

This project is a library that provides functionalities to load [Monster Hunter: World][monster-hunter-world] master data.

Note that for the moment the project is not complete, and not all data is covered.

# How it works

## Input data

Data from MHW are stored in files named `chunk*.bin`, those files are compressed and encrypted. This project works on decrypted and decompressed files.

In order to decrypt and decompress the `.bin` files, use [WorldChunkTool][world-chunk-tool]. It produces files named `chunk*.pkg`, which are the input for `MHWMasterDataUtils`.

Note that [WorldChunkTool][world-chunk-tool] also propose to extract the `.pkg` files (outputs tons of small files) but this is not required, since `MHWMasterDataUtils` directly works on `.pkg` for more convenience. Moreover, extraction of the inner files is an  extremely long process.

## The library

The main project is `MHWMasterDataUtils`, and the main types are the ones inheriting from `IPackageProcessor`, the `PackageReader` and all types in the `MHWMasterDataUtils.Builders` namespace.

You can have a look at the project `MHWMasterDataUtils.Exporter` to see how to use the library, this is a good sample project.

### Package processors

The `PackageReader` type exposes only a constructor and a `Run` method. The constructor takes an optional logger and a collection of `IPackageProcessor`, which are the one doing all the hard work. The `Run` method takes a path, where is will look for all the `.pkg` files and run the `IPackageProcessor`s over those files, in order from newest to oldest.

Many `IPackageProcessor` type are already provided:

- Base processors:
    - `PackageProcessorBase`: Provides a default implementation for non-critical `IPackageProcessor` methods (can be replaced by interface default implementation when C# 8 becomes mainstream)
    - `ListPackageProcessorBase`: Provides an implementation to load entries of a custom type based on a common pattern, as a list of entries. Duplicates or older entries are accumulated.
    - `MapPackageProcessorBase`: Provides an implementation to load entries of a custom type based on a common pattern, as a dictionary of entries. Duplicates or older entries are skipped.
- Specific processors:
    - `ArmorPackageProcessor`: Loads armor pieces.
    - `CraftPackageProcessor`: Loads information required for equipment craft and upgrades.
    - `ItemsPackageProcessor`: Loads all items.
    - `JewelPackageProcessor`: Loads jewels.
    - `LanguagePackageProcessor`: Loads translation texts for a given file pattern.
    - `SharpnessPackageProcessor`: Loads melee weapons sharpness.
    - `SkillAbilitiesPackageProcessor`: Loads skill abilities (detailed levels of a skill, e.g. 7 levels of Attack).
    - `SkillsPackageProcessor`: Loads skills.
    - `AmmoPackageProcessor`: Loads bowguns shells information.
    - `SwitchAxePhialPackageProcessor`: Loads switch axes phials information.
    - `BowBottleTablePackageProcessor`: Loads bow coatings.
    - `DualBladesSpecialPackageProcessor`: Loads dual blades specific information (double elements).
    - `GunlanceShellPackageProcessor`: Loads shelling information specific to the gunlance.
    - `HuntingHornNotesPackageProcessor`: Loads hunting horns notes.
    - `HuntingHornSongsPackageProcessor`: Loads hunting horns songs.
    - `WeaponsPackageProcessor`: Loads weapons data, per weapon type.
    - `WeaponUpgradePackageProcessor`: Loads weapon trees.

One can reuse the base processors to implement new ones.

### Builders

The "builders" are types that use intermediate data extracted by `IPackageProcessor`s to produce higher level data, such as data exported to JSON files. They do not inherit from any type. However, builders for weapons and armors are still organized in type hierarchy.

- `WeaponBuilderBase`: Base builder for all weapon builders.
    - `MeleeWeaponWeaponBuilderBase`: Base builder for all melee weapon builders.
        - `ChargeBladeWeaponBuilder`: Builds charge blades.
        - `DualBladesWeaponBuilder`: Builds dual blades.
        - `GreatSwordWeaponBuilder`: Builds great swords.
        - `GunlanceWeaponBuilder`: Builds gunlances.
        - `HammerWeaponBuilder`: Builds hammers.
        - `HuntingHornWeaponBuilder`: Builds hunting horns.
        - `InsectGlaiveWeaponBuilder`: Builds insect glaives.
        - `LanceWeaponBuilder`: Builds lances.
        - `LongSwordWeaponBuilder`: Builds long swords.
        - `SwitchAxeWeaponBuilder`: Builds switch axes.
        - `SwordAndShieldWeaponBuilder`: Builds swords and shields.
    - `BowWeaponBuilder`: Builds bows.
    - `BowgunWeaponBuilder`: Builds heavy and light bowguns.
- `EquipmentBuilderBase`: Base builder for equipment.
    - `ArmorPieceEquipmentBuilder`: Builds heads, chests, arms, waists and legs.
    - `CharmEquipmentBuilder`: Builds charms.
- `ItemBuilder`: Builds items.
- `SkillBuilder`: Builds skills.
- `WeaponTreeNameBuilder`: Builds weapon tree names.
- `ArmorSeriesBuilder`: Builds armor series names.
- `JewelBuilder`: Builds jewels.

### Core types

The solution contains a project named `MHWMasterDataUtils.Core`, which holds the high level types required to load exported data. The reason for this library to be lose-coupled from the rest is because it is allows to load this assembly in tools without loading all package processors, builders and other "offline" stuffs.

# Terms of use

[Monster Hunter™][monster-hunter] and [Monster Hunter: World™][monster-hunter-world] are registered trademarks or trademarks of [Capcom Co., Ltd][capcom].

[monster-hunter]: https://en.wikipedia.org/wiki/Monster_Hunter
[monster-hunter-world]: http://www.monsterhunterworld.com
[capcom]: http://www.capcom.com
[world-chunk-tool]: https://github.com/mhvuze/WorldChunkTool
