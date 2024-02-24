namespace Dnd.System.Entities.DiceModifiers;

[Flags]
public enum EHitResult : byte
{
    None = 0,
    RegularHit = 1,
    CriticalHit = 2
}
