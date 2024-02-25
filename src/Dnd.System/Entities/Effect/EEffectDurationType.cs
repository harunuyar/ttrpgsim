namespace Dnd.System.Entities.Effect;

[Flags]
public enum EEffectDurationType : int
{
    None = 0,
    Permanent = 1,
    UntilDispelled = 2,
    UntilTriggered = 4,
    UntilShortRest = 8,
    UntilLongRest = 16,
    UntilBroken = 32,
    Instantaneous = 64,
    Timed = 128,
    Special = 256,
}
