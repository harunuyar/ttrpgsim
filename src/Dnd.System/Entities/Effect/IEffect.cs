namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public enum EffectDurationType : byte
{
    None = 0,
    Permanent = 1,
    UntilDispelled = 2,
    UntilTriggered = 4,
    UntilShortRest = 8,
    UntilLongRest = 16,
    UntilBroken = 32,
    Instantaneous = 64,
    Timed = 128
}

public interface IEffect : IBonusProvider
{
    string Name { get; }

    string Description { get; }

    EffectDurationType DurationType { get; }

    IGameActor Source { get; }

    IGameActor Target { get; }

    TimeSpan? Duration { get; }
    
    TimeSpan? RemainingDuration { get; }

    int? MaxTriggerCount { get; }

    int? RemainingTriggerCount { get; }

    int? MaxRestCount { get; }

    int? RemainingRestCount { get; }
}
