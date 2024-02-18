namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class GetSpellAttackModifier : GetAttackModifier
{
    public GetSpellAttackModifier(IGameActor character, IAttackingSpell spell, IGameActor? target) : base(character, target)
    {
        Spell = spell;
    }

    public IAttackingSpell Spell { get; }

    protected override void InitializeResult()
    {
        base.InitializeResult();

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

        if (Target != null)
        {
            var attackModifierAgainst = new GetSpellAttackModifierAgainst(Target, Spell, Actor).Execute();

            if (!attackModifierAgainst.IsSuccess)
            {
                SetErrorAndReturn("GetSpellAttackModifierAgainst: " + attackModifierAgainst.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Attack Modifier From Target", attackModifierAgainst);
        }
    }
}
