namespace Dnd.Entities.Items.Equipments.Weapons;

public enum EWeaponProperty : int
{
    Ammunition      = 0x0001,
    Finesse         = 0x0002,
    Heavy           = 0x0004,
    Light           = 0x0008,
    Loading         = 0x0010,
    Range           = 0x0020,
    Reach           = 0x0040,
    Special         = 0x0080,
    Thrown          = 0x0100,
    TwoHanded       = 0x0200,
    Versatile       = 0x0400
}
