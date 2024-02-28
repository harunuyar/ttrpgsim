namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class Restrained : AConditionEffect
{
    public static async Task<Restrained?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Restrained);

        if (conditionModel == null)
        {
            return null;
        }

        return new Restrained(conditionModel);
    }

    private Restrained(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is GetSpeed getSpeed)
        {
            getSpeed.SetBaseValueAndReturn(0, Name);
        }

        return Task.CompletedTask;
    }
}
