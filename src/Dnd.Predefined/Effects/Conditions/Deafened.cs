namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class Deafened : AConditionEffect
{
    public static async Task<Deafened?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Deafened);

        if (conditionModel == null)
        {
            return null;
        }

        return new Deafened(conditionModel);
    }

    private Deafened(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        return Task.CompletedTask;
    }
}
