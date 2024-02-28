namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class Charmed : AConditionEffect
{
    public static async Task<Charmed?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Charmed);

        if (conditionModel == null)
        {
            return null;
        }

        return new Charmed(conditionModel);
    }

    private Charmed(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is CanAttackTarget canAttackTarget && canAttackTarget.Target == effectSource)
        {
            canAttackTarget.SetValue(false, "You can't directly harm your charmer.");
        }

        return Task.CompletedTask;
    }
}
