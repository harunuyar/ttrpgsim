namespace Dnd.Entities.Items.Equipments.Weapons;

public enum EWeaponType : long
{
    None                = 0x0000000000000000,
    // Simple Melee Weapons
    Club                = 0x0000000000000001,
    Dagger              = 0x0000000000000002,
    Greatclub           = 0x0000000000000004,
    Handaxe             = 0x0000000000000008,
    Javelin             = 0x0000000000000010,
    LightHammer         = 0x0000000000000020,
    Mace                = 0x0000000000000040,
    Quarterstaff        = 0x0000000000000080,
    Sickle              = 0x0000000000000100,
    Spear               = 0x0000000000000200,
    // Simple Ranged Weapons
    CrossbowLight       = 0x0000000000000400,
    Dart                = 0x0000000000000800,
    Shortbow            = 0x0000000000001000,
    Sling               = 0x0000000000002000,
    // Martial Melee Weapons
    Battleaxe           = 0x0000000000004000,
    Flail               = 0x0000000000008000,
    Glaive              = 0x0000000000010000,
    Greataxe            = 0x0000000000020000,
    Greatsword          = 0x0000000000040000,
    Halberd             = 0x0000000000080000,
    Lance               = 0x0000000000100000,
    Longsword           = 0x0000000000200000,
    Maul                = 0x0000000000400000,
    Morningstar         = 0x0000000000800000,
    Pike                = 0x0000000001000000,
    Rapier              = 0x0000000002000000,
    Scimitar            = 0x0000000004000000,
    Shortsword          = 0x0000000008000000,
    Trident             = 0x0000000010000000,
    WarPick             = 0x0000000020000000,
    Warhammer           = 0x0000000040000000,
    Whip                = 0x0000000080000000,
    // Martial Ranged Weapons
    Blowgun             = 0x0000000100000000,
    CrossbowHand        = 0x0000000200000000,
    CrossbowHeavy       = 0x0000000400000000,
    Longbow             = 0x0000000800000000,
    Net                 = 0x0000001000000000,
    // All Simple Melee Weapons
    SimpleMeleeWeapon = 0x00000000000003ff,
    // All Simple Ranged Weapons
    SimpleRangedWeapon  = 0x0000000000003c00,
    // All Simple Weapons
    SimpleWeapon        = SimpleMeleeWeapon | SimpleRangedWeapon,
    // All Martial Melee Weapons
    MartialMeleeWeapon  = 0x00000000ffffc000,
    // All Martial Ranged Weapons
    MartialRangedWeapon = 0x0000001f00000000,
    // All Martial Weapons
    MartialWeapon       = MartialMeleeWeapon | MartialRangedWeapon,
    // All Weapons
    All                 = SimpleWeapon | MartialWeapon
}


