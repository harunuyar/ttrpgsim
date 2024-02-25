namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class Charmed : AConditionEffect
{
    public static async Task<Charmed?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Charmed);

        if (conditionModel == null)
        {
            return null;
        }

        return new Charmed(conditionModel, durationType, source, target);
    }

    private Charmed(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target) 
        : base(conditionModel, durationType, source, target)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is CanAttackTarget canAttackTarget && canAttackTarget.Target == Source)
        {
            canAttackTarget.SetValue(false, "You can't directly harm your charmer.");
        }

        return base.HandleCommand(command);
    }
}
