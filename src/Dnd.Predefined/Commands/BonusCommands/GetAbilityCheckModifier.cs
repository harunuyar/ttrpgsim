namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetAbilityCheckModifier : ListCommand<int>
{
    public GetAbilityCheckModifier(IGameActor character, IAbilityCheckAction abilityCheckAction) : base(character)
    {
        AbilityCheckAction = abilityCheckAction;
    }

    public IAbilityCheckAction AbilityCheckAction { get; }

    protected override async Task InitializeResult()
    {
        var attributeModifierResult = await new GetAbilityModifier(Actor, AbilityCheckAction.Ability).Execute();

        if (!attributeModifierResult.IsSuccess)
        {
            SetError("GetAbilityModifier: " + attributeModifierResult.ErrorMessage);
            return;
        }

        AddValue(attributeModifierResult.Value, attributeModifierResult.Message);

        var hasAbilityProficiency = await new HasAbilityProficiency(Actor, AbilityCheckAction.Ability).Execute();

        if (!hasAbilityProficiency.IsSuccess)
        {
            SetError("HasSavingThrowProficiency: " + hasAbilityProficiency.ErrorMessage);
        }

        if (hasAbilityProficiency.Value)
        {
            var proficiencyBonusResult = await new GetProficiencyBonus(Actor).Execute();

            if (!proficiencyBonusResult.IsSuccess)
            {
                SetError("GetProficiencyBonus: " + proficiencyBonusResult.ErrorMessage);
            }

            AddValue(proficiencyBonusResult.Value, proficiencyBonusResult.Message);
        }
    }
}
