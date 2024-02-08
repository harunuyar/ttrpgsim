namespace DnD.Entities.Items.Equipments.Armors;

internal enum EArmorType : byte
{
    None = 0,
    Light = 1,
    Medium = 2,
    Heavy = 4,
    Shield = 8,
    All = Light | Medium | Heavy | Shield
}
