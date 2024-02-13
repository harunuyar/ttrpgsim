using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

namespace Dnd.System.Entities.Effects;

public interface IEffect : IBonusProvider
{
    string Description { get; }

    IEffectDuration Duration { get; }

    IGameActor Source { get; }

    IGameActor Target { get; }

    void StartEffect()
    {
        Source.EffectsTable.AddCausedEffect(this);
    }

    void RemoveEffect()
    {
        Source.EffectsTable.RemoveCausedEffect(this);
    }
}
