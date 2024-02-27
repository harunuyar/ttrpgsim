namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class Exhaustion : AConditionEffect
{
    public static async Task<Exhaustion?> Create(IGameActor source, IGameActor target, int level, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Exhaustion);

        if (conditionModel == null)
        {
            return null;
        }

        return new Exhaustion(conditionModel, durationType, source, target, level);
    }

    private Exhaustion(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target, int level)
        : base(conditionModel, durationType, source, target)
    {
        Level = level;
    }

    public int Level { get; set; }

    public override Task HandleCommand(ICommand command)
    {
        if (command is GetAdvantage advantage)
        {
            if (Level >= 1 && (advantage.Action is IAbilityCheckAction || advantage.Action is ISkillCheckAction))
            {
                advantage.AddValue(EAdvantage.Disadvantage, Name + " Level " + Level);
            }
            else if (Level >= 3 && (advantage.Action is IAttackAction || advantage.Action is ISavingThrowAction))
            {
                advantage.AddValue(EAdvantage.Disadvantage, Name + " Level " + Level);
            }
        }

        if (command is GetSpeed speed)
        {
            if (Level >= 2)
            {
                speed.AddFinalAction(() => speed.AddBonus(-speed.GetCurrentValue()/2, Name + " Level " + Level));
            }
            else if (Level >= 5)
            {
                speed.AddFinalAction(() => speed.AddBonus(-speed.GetCurrentValue(), Name + " Level " + Level));
            }
        }

        if (command is GetMaxHP maxHP)
        {
            if (Level >= 4)
            {
                maxHP.AddFinalAction(() => maxHP.AddBonus(-maxHP.GetCurrentValue()/2, Name + " Level " + Level));
            }
            else if (Level >= 6)
            {
                maxHP.AddFinalAction(() => maxHP.AddBonus(-maxHP.GetCurrentValue(), Name + " Level " + Level));
            }
        }

        return base.HandleCommand(command);
    }
}