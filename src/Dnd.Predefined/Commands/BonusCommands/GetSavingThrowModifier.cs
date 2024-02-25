namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetSavingThrowModifier : ListCommand<int>
{
    public GetSavingThrowModifier(IGameActor character, ISavingThrowAction savingThrowAction) : base(character)
    {
        SavingThrowAction = savingThrowAction;
    }

    public ISavingThrowAction SavingThrowAction { get; }

    protected override async Task InitializeResult()
    {
        var attributeModifierResult = await new GetAbilityModifier(Actor, SavingThrowAction.Ability).Execute();

        if (!attributeModifierResult.IsSuccess)
        {
            SetError("GetAbilityModifier: " + attributeModifierResult.ErrorMessage);
            return;
        }

        AddValue(attributeModifierResult.Value, attributeModifierResult.Message);

        var hasSavingThrowProficiency = await new HasSavingThrowProficiency(Actor, SavingThrowAction.Ability).Execute();

        if (!hasSavingThrowProficiency.IsSuccess)
        {
            SetError("HasSavingThrowProficiency: " + hasSavingThrowProficiency.ErrorMessage);
        }

        if (hasSavingThrowProficiency.Value)
        {
            var proficiencyBonusResult = await new GetProficiencyBonus(Actor).Execute();

            if (!proficiencyBonusResult.IsSuccess)
            {
                SetError("GetProficiencyBonus: " + proficiencyBonusResult.ErrorMessage);
            }

            AddValue(proficiencyBonusResult.Value, proficiencyBonusResult.Message);
        }

        if (SavingThrowAction.ActionOwner is not null)
        {
            var against = await new GetAttackSavingThrowModifierAgainst(SavingThrowAction.ActionOwner, Actor, SavingThrowAction).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetAttackSavingThrowModifierAgainst: " + against.ErrorMessage);
                return;
            }

            Add(against);
        }
    }
}
