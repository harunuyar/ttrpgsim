namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
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
            var getAttributeModifier = new GetAttributeModifier(command.Character, SpellCasterClass.SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellAttackModifier.GetResult().BaseValue <= attributeModifier.Value)
                {
                    calculateSpellAttackModifier.SetBaseValue(this, attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellAttackModifier.SetErrorAndReturn("Couldn't get attribute modifier: " + attributeModifier.ErrorMessage);
            }
        }
        else if (command is CalculateSpellSavingDifficultyClass calculateSpellSavingDifficultyClass)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Character, SpellCasterClass.SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellSavingDifficultyClass.GetResult().BaseValue <= attributeModifier.Value)
                {
                    calculateSpellSavingDifficultyClass.SetBaseValue(this, attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellSavingDifficultyClass.SetErrorAndReturn("Couldn't get attribute modifier" + attributeModifier.ErrorMessage);
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
