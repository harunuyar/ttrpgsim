namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class Prone : AConditionEffect
{
    public static async Task<Prone?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Prone);

        if (conditionModel == null)
        {
            return null;
        }

        return new Prone(conditionModel);
    }

    private Prone(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        return Task.CompletedTask;
    }
}
