namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class SavingThrowAction : SuccessRollAction, ISavingThrowAction
{
    public SavingThrowAction(string name, AbilityScoreModel ability, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, ESuccessRollType.Save, usageLimits)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        if (command is GetModifiers modifiers)
        {
            var attributeModifierResult = await new GetAbilityModifier(modifiers.Actor, Ability).Execute();

            if (!attributeModifierResult.IsSuccess)
            {
                modifiers.SetError("GetAbilityModifier: " + attributeModifierResult.ErrorMessage);
                return;
            }

            modifiers.AddValue(attributeModifierResult.Value, attributeModifierResult.Message);

            var hasSavingThrowProficiency = await new HasSavingThrowProficiency(modifiers.Actor, Ability).Execute();

            if (!hasSavingThrowProficiency.IsSuccess)
            {
                modifiers.SetError("HasSavingThrowProficiency: " + hasSavingThrowProficiency.ErrorMessage);
            }

            if (hasSavingThrowProficiency.Value)
            {
                var proficiencyBonusResult = await new GetProficiencyBonus(modifiers.Actor).Execute();

                if (!proficiencyBonusResult.IsSuccess)
                {
                    modifiers.SetError("GetProficiencyBonus: " + proficiencyBonusResult.ErrorMessage);
                }

                modifiers.AddValue(proficiencyBonusResult.Value, proficiencyBonusResult.Message);
            }
        }
    }
}
