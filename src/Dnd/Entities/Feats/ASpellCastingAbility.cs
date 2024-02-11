namespace Dnd.Entities.Feats;

using Dnd.CommandSystem.Commands;
using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.Entities.Attributes;

public abstract class ASpellCastingAbility : AFeat
{
    public ASpellCastingAbility(string name, string description, EAttributeType spellCastingAttribute) : base(name, description)
    {
        SpellCastingAttribute = spellCastingAttribute;
    }

    public EAttributeType SpellCastingAttribute { get; }

    public override void HandleCommand(DndCommand command)
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
                calculateSpellAttackModifier.Result.SetBaseValue(command.Character.AttributeSet.GetAttribute(SpellCastingAttribute), attributeModifier.Value);
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
                calculateSpellSavingDifficultyClass.Result.SetBaseValue(command.Character.AttributeSet.GetAttribute(SpellCastingAttribute), attributeModifier.Value);
            }
            else
            {
                calculateSpellSavingDifficultyClass.Result.SetError("Couldn't get attribute modifier");
            }
        }
    }
}
