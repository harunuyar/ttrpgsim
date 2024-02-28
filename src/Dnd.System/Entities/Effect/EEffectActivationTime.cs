namespace Dnd.System.Entities.Effect;

[Flags]
public enum EEffectActivationTime : int
{
    None = 0,
    Instant = 1,
    SourceTurnStart = 2,
    SourceTurnEnd = 4,
    TargetTurnStart = 8,
    TargetTurnEnd = 16,
    LongRest = 32,
    ShortRest = 64,
    TargetEnteredArea = 128,
    TargetLeftArea = 256,
    Manual = 512,
}
