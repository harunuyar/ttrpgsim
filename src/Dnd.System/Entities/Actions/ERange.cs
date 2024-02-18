namespace Dnd.System.Entities.Actions;

[Flags]
public enum ERange : byte
{
    None = 0,
    Self = 1,
    Touch = 2,
    Range = 4,
    Area = 8,
    SelfArea = Self | Area,
    RangeArea = Range | Area
}
