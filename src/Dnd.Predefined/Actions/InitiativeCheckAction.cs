namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public class InitiativeCheckAction : AmountAction, IInitiativeCheckAction
{
    public InitiativeCheckAction() 
        : base("Initiative Check", ActionDurationType.FreeAction, new DicePool([new DiceRoll(1, EDiceType.d20)], 0), EAmountRollType.Initiative, [new ActionUsageLimit(EActionUsageLimitType.PerCombat, 1)])
    {
    }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        if (command is GetModifiers modifiers)
        {
            var abilityScore = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);
            if (abilityScore == null)
            {
                modifiers.SetError("AbilityScoreModel not found for " + AbilityScores.Dex);
                return;
            }

            var dexterityModifierResult = await new GetAbilityModifier(modifiers.Actor, abilityScore).Execute();

            if (!dexterityModifierResult.IsSuccess)
            {
                modifiers.SetError("GetAbilityModifier: " + dexterityModifierResult.ErrorMessage);
            }

            modifiers.AddValue(dexterityModifierResult.Value, dexterityModifierResult.Message);
        }
    }
}
