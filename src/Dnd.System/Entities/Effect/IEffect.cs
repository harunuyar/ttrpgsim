namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public interface IEffect : ICommandHandler
{
    string Name { get; }
    string Description { get; }
    EffectDuration Duration { get; }
    IGameActor Source { get; }
    Duration? RemainingDuration { get; }
    int? RemainingTriggerCount { get; }
    int? RemainingRestCount { get; }
    bool IsExpired { get; }
    Task ActivateEffect();
}
