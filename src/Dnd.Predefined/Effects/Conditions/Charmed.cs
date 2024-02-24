namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public class Charmed : AConditionEffect
{
    public static async Task<Charmed?> Create(IGameActor source, IGameActor target, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Charmed);

        if (conditionModel == null)
        {
            return null;
        }

        return new Charmed(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount);
    }

    private Charmed(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null) 
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
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
