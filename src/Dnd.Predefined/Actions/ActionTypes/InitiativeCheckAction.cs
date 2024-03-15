namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Events;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class InitiativeCheckAction : EventAction, IInitiativeCheckAction
{
    public InitiativeCheckAction()
        : base("Initiative Check", ActionDurationType.FreeAction, [new ActionUsageLimit(EActionUsageLimitType.PerCombat, 1)])
    {
    }

    public DicePool AmountDicePool => new DicePool([new DiceRoll(1, EDiceType.d20)], 0);

    public EAmountRollType AmountRollType => EAmountRollType.Initiative;

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new RollAmountEvent(Name, actor, this, null));
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
