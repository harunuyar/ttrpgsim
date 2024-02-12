using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

namespace Dnd.System.Entities.Effects;

public interface IEffect : IBonusProvider
{
    string Description { get; }

    IEffectDuration Duration { get; }

    ICharacter Source { get; }

    ICharacter Target { get; }

    void StartEffect()
    {
        Source.EffectsTable.AddCausedEffect(this);
    }

    void RemoveEffect()
    {
        Source.EffectsTable.RemoveCausedEffect(this);
    }
}
