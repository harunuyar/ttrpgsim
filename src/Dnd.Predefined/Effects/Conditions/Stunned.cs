namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class Stunned : AConditionEffect
{
    public static async Task<Stunned?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Stunned);

        if (conditionModel == null)
        {
            return null;
        }

        return new Stunned(conditionModel, durationType, source, target);
    }

    private Stunned(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target) 
        : base(conditionModel, durationType, source, target)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(false, Name);
        }

        return base.HandleCommand(command);
    }
}
