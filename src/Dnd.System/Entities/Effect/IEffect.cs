namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.GameActor;

public interface IEffect : ICommandHandler
{
    IEffectDefinition EffectDefinition { get; }
    EffectDurationInstance Duration { get; }
    IGameActor Source { get; }
    bool IsExpired { get; }
    void ActivateEffect();
}
