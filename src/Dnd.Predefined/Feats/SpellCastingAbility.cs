namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class SpellCastingAbility : AFeat
{
    public SpellCastingAbility(string name, string description, EAttributeType spellCastingAttribute) : base(name, description)
    {
        SpellCastingAttribute = spellCastingAttribute;
    }

    public EAttributeType SpellCastingAttribute { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is CanCastSpell canCastSpell)
        {
            canCastSpell.Result.SetValue(this, true);
        }
        else if (command is CalculateSpellAttackModifier calculateSpellAttackModifier)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Character, SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellAttackModifier.Result.BaseValue <= attributeModifier.Value)
                {
                    calculateSpellAttackModifier.Result.SetBaseValue(command.Character.AttributeSet.GetAttribute(SpellCastingAttribute), attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellAttackModifier.Result.SetError("Couldn't get attribute modifier");
            }
        }
        else if (command is CalculateSpellSavingDifficultyClass calculateSpellSavingDifficultyClass)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Character, SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellSavingDifficultyClass.Result.BaseValue <= attributeModifier.Value)
                {
                    calculateSpellSavingDifficultyClass.Result.SetBaseValue(command.Character.AttributeSet.GetAttribute(SpellCastingAttribute), attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellSavingDifficultyClass.Result.SetError("Couldn't get attribute modifier");
            }
        }
    }
}
