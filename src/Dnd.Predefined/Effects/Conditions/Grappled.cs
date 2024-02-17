namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

public class Grappled : AEffect
{
    public Grappled(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Grappled", "A grappled creature's speed becomes 0, and it can't benefit from any bonus to its speed.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetSpeed getSpeed)
        {
            getSpeed.SetValueAndReturn(this, 0);
        }
    }
}
