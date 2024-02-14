namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Classes;

public abstract class SpellCastingAbility : AFeat
{
    public SpellCastingAbility(string name, string description, IClass spellCasterClass) : base(name, description)
    {
        SpellCasterClass = spellCasterClass;
    }

    public IClass SpellCasterClass { get; }

    public abstract void HandleCantripsCount(GetKnownCantripsCount getKnownCantripsCount);

    public abstract void HandleKnownSpellsCount(GetKnownSpellsCount getKnownSpellsCount);

    public abstract void HandleSpellSlotsCount(GetSpellSlotsCount getSpellSlotsCount);

    public override void HandleCommand(ICommand command)
    {
        if (command is CanCastSpell canCastSpell)
        {
            canCastSpell.SetValue(this, true);
        }
        else if (command is CalculateSpellAttackModifier calculateSpellAttackModifier)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Actor, SpellCasterClass.SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellAttackModifier.Result.BaseValue <= attributeModifier.Value)
                {
                    calculateSpellAttackModifier.SetBaseValue(this, attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellAttackModifier.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            var getProficiencyBonus = new GetProficiencyBonus(command.Actor);
            var proficiencyBonus = getProficiencyBonus.Execute();

            if (proficiencyBonus.IsSuccess)
            {
                calculateSpellAttackModifier.AddBonus(new CustomDndEntity("Proficiency Bonus"), proficiencyBonus.Value);
            }
            else
            {
                calculateSpellAttackModifier.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }
        }
        else if (command is CalculateSpellSavingDifficultyClass calculateSpellSavingDifficultyClass)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Actor, SpellCasterClass.SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellSavingDifficultyClass.Result.BaseValue <= attributeModifier.Value)
                {
                    calculateSpellSavingDifficultyClass.SetBaseValue(this, attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellSavingDifficultyClass.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            var getProficiencyBonus = new GetProficiencyBonus(command.Actor);
            var proficiencyBonus = getProficiencyBonus.Execute();

            if (proficiencyBonus.IsSuccess)
            {
                calculateSpellSavingDifficultyClass.AddBonus(new CustomDndEntity("Proficiency Bonus"), proficiencyBonus.Value);
            }
            else
            {
                calculateSpellSavingDifficultyClass.SetErrorAndReturn(proficiencyBonus.ErrorMessage ?? "Couldn't get proficiency bonus");
            }
        }
        else if (command is GetKnownCantripsCount getCantripsCount)
        {
            HandleCantripsCount(getCantripsCount);
        }
        else if (command is GetKnownSpellsCount getSpellsCount)
        {
            HandleKnownSpellsCount(getSpellsCount);
        }
        else if (command is GetSpellSlotsCount getSpellSlotsCount)
        {
            HandleSpellSlotsCount(getSpellSlotsCount);
        }
    }
}
