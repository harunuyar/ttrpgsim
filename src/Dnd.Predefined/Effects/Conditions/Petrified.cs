namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;

public class Petrified : AEffect
{
    public Petrified(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Petrified", "A petrified creature is transformed, along with any nonmagical object it is wearing or carrying, into a solid inanimate substance (usually stone). Its weight increases by a factor of ten, and it ceases aging.", 
            duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValueAndReturn(this, false, "You are petrified.");
        }
    }
}
