namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;

public class Incapacitated : AEffect
{
    public Incapacitated(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Incapacitated", "An incapacitated creature can't take actions or reactions.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(this, false, "You can't take any action or reaction while incapacitated.");
        }
    }
}
