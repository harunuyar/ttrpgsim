namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class GetSpellSavingDC : DndScoreCommand
{
    public GetSpellSavingDC(IGameActor character, IAttackingSpell spell) : base(character)
    {
        Spell = spell;
    }

    public IAttackingSpell Spell { get; }

    protected override void InitializeResult()
    {
        if (Spell.SuccessMeasuringType != ESuccessMeasuringType.SavingThrow)
        {
            SetErrorAndReturn("Spell doesn't require saving throw");
            return;
        }

        Result.SetBaseValue("Base", 8);

        var spellCasterAttribute = new GetSpellCasterAttribute(Actor).Execute();

        if (!spellCasterAttribute.IsSuccess)
        {
            SetErrorAndReturn("GetSpellCasterAttribute: " + spellCasterAttribute.ErrorMessage);
            return;
        }

        if (spellCasterAttribute.Value == EAttributeType.None)
        {
            SetErrorAndReturn("Spell caster attribute is None");
            return;
        }

        var attributeModifier = new GetAttributeModifier(Actor, spellCasterAttribute.Value).Execute();

        if (!attributeModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
            return;
        }

        Result.AddAsBonus(Actor.AttributeSet.GetAttribute(spellCasterAttribute.Value), attributeModifier);

        var proficiencyBonus = new GetProficiencyBonus(Actor).Execute();

        if (!proficiencyBonus.IsSuccess)
        {
            SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
            return;
        }

        Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
    }
}
