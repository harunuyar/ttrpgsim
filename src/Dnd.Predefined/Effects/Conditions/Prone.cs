namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Prone : AEffect
{
    public Prone(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Prone", "A prone creature's only movement option is to crawl, unless it stands up and thereby ends the condition.", duration, source, target)
    {
    }
}
