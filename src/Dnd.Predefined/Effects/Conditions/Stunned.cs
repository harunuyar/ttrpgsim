namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;

public class Stunned : AEffect
{
    public Stunned(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Stunned", "A stunned creature is incapacitated, can't move, and can speak only falteringly.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValueAndReturn(this, false, "You are stunned and can't take any action.");
        }
    }
}
