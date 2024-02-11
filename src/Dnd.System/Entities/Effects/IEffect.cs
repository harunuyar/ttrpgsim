using Dnd.System.Entities.Effects.Duration;

namespace Dnd.System.Entities.Effects;

public interface IEffect : IBonusProvider
{
    public string Description { get; }

    public IEffectDuration Duration { get; }
}
