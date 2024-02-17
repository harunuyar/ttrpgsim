namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;

public class Unconscious : AEffect
{
    public Unconscious(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Unconscious", "An unconscious creature is incapacitated, can't move or speak, and is unaware of its surroundings.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValueAndReturn(this, false, "You are unconscious and can't take any action.");
        }
    }
}
