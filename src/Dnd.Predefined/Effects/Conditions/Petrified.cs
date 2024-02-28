namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class Petrified : AConditionEffect
{
    public static async Task<Petrified?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Petrified);

        if (conditionModel == null)
        {
            return null;
        }

        return new Petrified(conditionModel);
    }

    private Petrified(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(false, Name);
        }

        return Task.CompletedTask;
    }
}
