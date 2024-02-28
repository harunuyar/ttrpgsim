namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class Incapacitated : AConditionEffect
{
    public static async Task<Incapacitated?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Incapacitated);

        if (conditionModel == null)
        {
            return null;
        }

        return new Incapacitated(conditionModel);
    }

    private Incapacitated(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(false, "You can't take any action or reaction while incapacitated.");
        }

        return Task.CompletedTask;
    }
}
