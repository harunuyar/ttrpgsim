namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class SpellAction : Action, ISpellAction
{
    public SpellAction(IGameActor actionOwner, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(actionOwner, spellModel.Name ?? "Unknown", ActionDurationType.FromString(spellModel.CastingTime) ?? ActionDurationType.Action, usageLimits)
    {
        SpellcastingAbility = spellcastingAbility;
        Spell = spellModel;
        SpellSlot = spellSlot;
    }

    public SpellModel Spell { get; }

    public int SpellSlot { get; }

    public ISpellcastingAbility SpellcastingAbility { get; }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);
        await SpellcastingAbility.HandleUsageCommand(command);

        if (command is IsActionAvailable isActionAvailable)
        {
            if (isActionAvailable.Action == this && SpellcastingAbility.HasSpell(Spell, SpellSlot))
            {
                var maxSpellSlots = await new GetMaxSpellSlotsCount(command.Actor, SpellSlot).Execute();

                if (!maxSpellSlots.IsSuccess)
                {
                    isActionAvailable.SetError("GetMaxSpellSlotsCount: " + maxSpellSlots.ErrorMessage);
                    return;
                }

                int usedSlots = command.Actor.PointsCounter.GetUsedSpellCounts(SpellSlot);

                if (usedSlots >= maxSpellSlots.Value)
                {
                    isActionAvailable.SetValue(false, $"{command.Actor.Name} can't cast {Spell.Name} at level {SpellSlot} because they don't have any spell slots for that level.");
                    return;
                }

                isActionAvailable.SetValue(true, $"{command.Actor.Name} can cast {Spell.Name} at level {SpellSlot}.");
            }
        }
        else if (command is GetSavingDC savingDC)
        {
            if (savingDC.SavingThrowAction == this)
            {
                var proficiencyBonus = await new GetProficiencyBonus(savingDC.Actor).Execute();

                if (!proficiencyBonus.IsSuccess)
                {
                    savingDC.SetError("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                    return;
                }

                savingDC.AddBonus(proficiencyBonus.Value, "Proficiency Bonus");

                var spellcasting = SpellcastingAbility.SubclassModel?.Spellcasting ?? SpellcastingAbility.ClassModel.Spellcasting;

                if (spellcasting?.SpellcastingAbility is null)
                {
                    savingDC.SetError($"Spellcasting ability is not set for {SpellcastingAbility.ClassModel.Name} {SpellcastingAbility.SubclassModel?.Name}");
                    return;
                }

                var ability = await spellcasting.SpellcastingAbility.GetModel<AbilityScoreModel>();
                if (ability is null)
                {
                    savingDC.SetError($"Ability {spellcasting.SpellcastingAbility.Url} couldn't be fetched");
                    return;
                }

                var spellcastingAbilityScore = await new GetAbilityModifier(savingDC.Actor, ability).Execute();

                if (!spellcastingAbilityScore.IsSuccess)
                {
                    savingDC.SetError("GetAbilityModifier: " + spellcastingAbilityScore.ErrorMessage);
                    return;
                }

                savingDC.AddBonus(spellcastingAbilityScore.Value, $"{(SpellcastingAbility.SubclassModel is not null ? SpellcastingAbility.SubclassModel.Name : SpellcastingAbility.ClassModel.Name)} Spellcasting Ability Modifier");
            }
        }
        else if (command is GetModifiers modifiers)
        {
            if (modifiers.Action == this && modifiers.Action is IAttackRollAction)
            {
                var proficiencyBonus = await new GetProficiencyBonus(modifiers.Actor).Execute();

                if (!proficiencyBonus.IsSuccess)
                {
                    modifiers.SetError("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                    return;
                }

                modifiers.AddValue(proficiencyBonus.Value, "Proficiency Bonus");

                var spellcasting = SpellcastingAbility.SubclassModel?.Spellcasting ?? SpellcastingAbility.ClassModel.Spellcasting;

                if (spellcasting?.SpellcastingAbility is null)
                {
                    modifiers.SetError($"Spellcasting ability is not set for {SpellcastingAbility.ClassModel.Name} {SpellcastingAbility.SubclassModel?.Name}");
                    return;
                }

                var ability = await spellcasting.SpellcastingAbility.GetModel<AbilityScoreModel>();
                if (ability is null)
                {
                    modifiers.SetError($"Ability {spellcasting.SpellcastingAbility.Url} couldn't be fetched");
                    return;
                }

                var spellcastingAbilityScore = await new GetAbilityModifier(modifiers.Actor, ability).Execute();

                if (!spellcastingAbilityScore.IsSuccess)
                {
                    modifiers.SetError("GetAbilityModifier: " + spellcastingAbilityScore.ErrorMessage);
                    return;
                }

                modifiers.AddValue(spellcastingAbilityScore.Value, $"{(SpellcastingAbility.SubclassModel is not null ? SpellcastingAbility.SubclassModel.Name : SpellcastingAbility.ClassModel.Name)} Spellcasting Ability Modifier");
            }
        }
    }
}
