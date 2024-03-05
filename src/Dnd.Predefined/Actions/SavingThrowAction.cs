namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SavingThrowAction : SuccessRollAction, ISavingThrowAction
{
    public SavingThrowAction(IGameActor actionOwner, AbilityScoreModel ability, double saveDamageMultiplier, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits)
        : base(actionOwner, $"{ability.FullName} Saving Throw", ActionDurationType.FreeAction, ERollType.Save, usageLimits)
    {
        Ability = ability;
        SaveDamageMultiplier = saveDamageMultiplier;
        DamageAction = new DamageAction(actionOwner, "DamageBonusCommands", ActionDurationType.FreeAction, ActionRange.Self, TargetingType.SingleTarget, damageType, damageDicePool, []);
    }

    public DamageAction DamageAction { get; }

    public AbilityScoreModel Ability { get; }

    public double SaveDamageMultiplier { get; }

    public DamageTypeModel DamageType => DamageAction.DamageType;

    public DicePool AmountDicePool => DamageAction.AmountDicePool;

    public ActionRange Range => DamageAction.Range;

    public TargetingType TargetingType => DamageAction.TargetingType;

    public Task<EAdvantage> GetAmountAdvantage()
    {
        return DamageAction.GetAmountAdvantage();
    }

    public Task<DicePool> GetAmountBonus()
    {
        return DamageAction.GetAmountBonus();
    }

    public Task<int> GetAmountResult(int defaultAmount)
    {
        return DamageAction.GetAmountResult(defaultAmount);
    }

    public Task<int?> GetPredeterminedAmount()
    {
        return DamageAction.GetPredeterminedAmount();
    }

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
