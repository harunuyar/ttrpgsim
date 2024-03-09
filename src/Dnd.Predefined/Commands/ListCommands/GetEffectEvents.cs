namespace Dnd.Predefined.Commands.ListCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class GetEffectEvents : ListCommand<IEffectEvent>
{
    public GetEffectEvents(IGameActor actor, EEffectActivationTime effectActivationTime) : base(actor)
    {
        EffectActivationTime = effectActivationTime;
    }

    public EEffectActivationTime EffectActivationTime { get; }
}
