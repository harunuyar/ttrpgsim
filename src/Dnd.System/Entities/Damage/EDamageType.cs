namespace Dnd.System.Entities.Damage;

[Flags]
public enum EDamageType : ushort
{
    None            = 0x0000,
    Acid            = 0x0001,
    Bludgeoning     = 0x0002,
    Cold            = 0x0004,
    Fire            = 0x0008,
    Force           = 0x0010,
    Lightning       = 0x0020,
    Necrotic        = 0x0040,
    Piercing        = 0x0080,
    Poison          = 0x0100,
    Psychic         = 0x0200,
    Radiant         = 0x0400,
    Slashing        = 0x0800,
    Thunder         = 0x1000,
}
