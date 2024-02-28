namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class AbilityCheckAction : RollAction, IAbilityCheckAction
{
    public AbilityCheckAction(IGameActor actionOwner, AbilityScoreModel ability) 
        : base(actionOwner, $"{ability.FullName} Check", ActionDurationType.Action, ERollType.AbilityCheck)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        if (command is GetModifiers modifiers)
        {
            var attributeModifierResult = await new GetAbilityModifier(command.Actor, Ability).Execute();

            if (!attributeModifierResult.IsSuccess)
            {
                modifiers.SetError("GetAbilityModifier: " + attributeModifierResult.ErrorMessage);
                return;
            }

            modifiers.AddValue(attributeModifierResult.Value, attributeModifierResult.Message);

            var hasAbilityProficiency = await new HasAbilityProficiency(command.Actor, Ability).Execute();

            if (!hasAbilityProficiency.IsSuccess)
            {
                modifiers.SetError("HasSavingThrowProficiency: " + hasAbilityProficiency.ErrorMessage);
            }

            if (hasAbilityProficiency.Value)
            {
                var proficiencyBonusResult = await new GetProficiencyBonus(command.Actor).Execute();

                if (!proficiencyBonusResult.IsSuccess)
                {
                    modifiers.SetError("GetProficiencyBonus: " + proficiencyBonusResult.ErrorMessage);
                }

                modifiers.AddValue(proficiencyBonusResult.Value, proficiencyBonusResult.Message);
            }
        }
    }
}
