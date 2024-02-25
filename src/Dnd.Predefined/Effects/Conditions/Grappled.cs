namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class Grappled : AConditionEffect
{
    public static async Task<Grappled?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Grappled);

        if (conditionModel == null)
        {
            return null;
        }

        return new Grappled(conditionModel, durationType, source, target);
    }

    private Grappled(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target) 
        : base(conditionModel, durationType, source, target)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is GetSpeed getSpeed)
        {
            getSpeed.SetBaseValueAndReturn(0, Name);
        }

        return base.HandleCommand(command);
    }
}
