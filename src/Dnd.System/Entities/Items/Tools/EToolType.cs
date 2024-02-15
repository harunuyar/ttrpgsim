namespace Dnd.System.Entities.Items.Tools;

public enum EToolType : long
{
    None            = 0x0000000000000000,
    // Artisan's Tools
    Alchemist       = 0x0000000000000001,
    Brewer          = 0x0000000000000002,
    Calligrapher    = 0x0000000000000004,
    Carpenter       = 0x0000000000000008,
    Cartographer    = 0x0000000000000010,
    Cobbler         = 0x0000000000000020,
    Cook            = 0x0000000000000040,
    Glassblower     = 0x0000000000000080,
    Jeweler         = 0x0000000000000100,
    Leatherworker   = 0x0000000000000200,
    Mason           = 0x0000000000000400,
    Painter         = 0x0000000000000800,
    Potter          = 0x0000000000001000,
    Smith           = 0x0000000000002000,
    Tinker          = 0x0000000000004000,
    Weaver          = 0x0000000000008000,
    Woodcarver      = 0x0000000000010000,
    // Gaming Sets
    Dice            = 0x0000000000020000,
    Dragonchess     = 0x0000000000040000,
    PlayingCard     = 0x0000000000080000,
    ThreeDragonAnte = 0x0000000000100000,
    // Musical Instruments
    Bagpipes        = 0x0000000000200000,
    Drum            = 0x0000000000400000,
    Dulcimer        = 0x0000000000800000,
    Flute           = 0x0000000001000000,
    Lute            = 0x0000000002000000,
    Lyre            = 0x0000000004000000,
    Horn            = 0x0000000008000000,
    PanFlute        = 0x0000000010000000,
    Shawm           = 0x0000000020000000,
    Viol            = 0x0000000040000000,
    // Other Tools
    DisguiseKit     = 0x0000000080000000,
    ForgeryKit      = 0x0000000100000000,
    HerbalismKit    = 0x0000000200000000,
    Navigator       = 0x0000000400000000,
    Poisoner        = 0x0000000800000000,
    Thieves         = 0x0000001000000000,
    // Categories
    Artisan         = 0x000000000001ffff,
    Gaming          = 0x00000000001e0000,
    Musical         = 0x000000007fe00000,
    Other           = 0x0000001f80000000,
    // All
    All             = 0x0000001fffffffff
}
