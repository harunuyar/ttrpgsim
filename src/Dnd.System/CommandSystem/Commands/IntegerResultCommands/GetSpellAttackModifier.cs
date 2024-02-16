namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class GetSpellAttackModifier : DndScoreCommand
{
    public GetSpellAttackModifier(IGameActor character, IAttackingSpell spell, IGameActor target) : base(character)
    {
        Spell = spell;
        Target = target;
    }

    public IAttackingSpell Spell { get; }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);

        if (Spell.SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
        {
            SetErrorAndReturn("Spell doesn't use attack roll");
            return;
        }

        var spellCasterAttribute = new GetSpellCasterAttribute(Actor).Execute();

        if (!spellCasterAttribute.IsSuccess)
        {
            SetErrorAndReturn("GetSpellCasterAttribute: " + spellCasterAttribute.ErrorMessage);
            return;
        }

        if (spellCasterAttribute.Value == Entities.Attributes.EAttributeType.None)
        {
            SetErrorAndReturn("SpellCasterAttribute is None");
            return;
        }

        var attributeModifier = new GetAttributeModifier(Actor, spellCasterAttribute.Value).Execute();

        if (!attributeModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
            return;
        }
            
        Result.AddAsBonus(attributeModifier.BaseSource ?? Actor.AttributeSet.GetAttribute(spellCasterAttribute.Value), attributeModifier);
        
        var proficiencyBonus = new GetProficiencyBonus(Actor).Execute();

        if (!proficiencyBonus.IsSuccess)
        {
            SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
            return;
        }

        Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
    }
}
