namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Class;
using Dnd.Context;
using Dnd.Predefined.Actions;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.GameManagers.Dice;

public class SecondWindAction : HealAction
{
    public SecondWindAction() 
        : base("Second Wind", ActionDurationType.BonusAction, ActionRange.Self, TargetingType.SingleTarget, new DicePool([new DiceRoll(1, EDiceType.d10)], 0), 
            [new ActionUsageLimit(EActionUsageLimitType.PerShortRest, 1)])
    {
    }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        if (command is GetAmountModifiers modifiers)
        {
            var classModel = await DndContext.Instance.GetObject<ClassModel>(Classes.Fighter);
            if (classModel == null)
            {
                throw new InvalidOperationException($"{Classes.Fighter} class model is not found");
            }

            var fighterLevel = command.Actor.LevelInfo.GetLevelsInClass(classModel);

            modifiers.AddValue(fighterLevel, Name);
        }
    }
}
