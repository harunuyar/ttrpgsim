namespace Dnd.System.Entities.Actions.Impl;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.Spells;
using Dnd.System.Entities.Damage;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.GameActors;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.Entities.Attributes;

public class SpellAttack : ASpellAction, IAttackAction
{
    public SpellAttack(IAttackingSpell attackingSpell, List<UsageLimitation> usageLimitations) : base(attackingSpell, usageLimitations)
    {
        AttackingSpell = attackingSpell;
    }

    public IAttackingSpell AttackingSpell { get; }

    public EDamageType DamageType => AttackingSpell.DamageType;

    public ESuccessMeasuringType SuccessMeasuringType => AttackingSpell.SuccessMeasuringType;

    public EDamageCalculationType DamageCalculationType => AttackingSpell.DamageCalculationType;

    public int? ConstantDamage => AttackingSpell.ConstantDamage;

    public DiceRoll? DamageDie => AttackingSpell.DamageDie;

    public EAttributeType? SavingThrowAttribute => AttackingSpell.SavingThrowAttribute;

    public Func<int, int>? FailureDamageModifier => AttackingSpell.FailureDamageModifier;

    public override void Apply(IGameActor actor, IEnumerable<IGameActor> targets)
    {
        AttackingSpell.ApplyEffect(actor, targets);
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetAttackModifier getAttackModifier)
        {
            if (getAttackModifier.AttackAction != this)
            {
                return;
            }

            var spellCasterAttribute = new GetSpellCasterAttribute(command.Actor).Execute();

            if (!spellCasterAttribute.IsSuccess)
            {
                getAttackModifier.SetErrorAndReturn("GetSpellCasterAttribute: " + spellCasterAttribute.ErrorMessage);
                return;
            }

            if (spellCasterAttribute.Value == Entities.Attributes.EAttributeType.None)
            {
                getAttackModifier.SetErrorAndReturn("SpellCasterAttribute is None");
                return;
            }

            var attributeModifier = new GetAttributeModifier(command.Actor, spellCasterAttribute.Value).Execute();

            if (!attributeModifier.IsSuccess)
            {
                getAttackModifier.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            getAttackModifier.Result.AddAsBonus(attributeModifier.BaseSource ?? command.Actor.AttributeSet.GetAttribute(spellCasterAttribute.Value), attributeModifier);

            var proficiencyBonus = new GetProficiencyBonus(command.Actor).Execute();

            if (!proficiencyBonus.IsSuccess)
            {
                getAttackModifier.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }

            getAttackModifier.Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
        }
        else if (command is GetSavingDC getSavingDC)
        {
            if (getSavingDC.AttackAction != this)
            {
                return;
            }

            var spellCasterAttribute = new GetSpellCasterAttribute(command.Actor).Execute();

            if (!spellCasterAttribute.IsSuccess)
            {
                getSavingDC.SetErrorAndReturn("GetSpellCasterAttribute: " + spellCasterAttribute.ErrorMessage);
                return;
            }

            if (spellCasterAttribute.Value == EAttributeType.None)
            {
                getSavingDC.SetErrorAndReturn("Spell caster attribute is None");
                return;
            }

            var attributeModifier = new GetAttributeModifier(command.Actor, spellCasterAttribute.Value).Execute();

            if (!attributeModifier.IsSuccess)
            {
                getSavingDC.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            getSavingDC.Result.AddAsBonus(command.Actor.AttributeSet.GetAttribute(spellCasterAttribute.Value), attributeModifier);

            var proficiencyBonus = new GetProficiencyBonus(command.Actor).Execute();

            if (!proficiencyBonus.IsSuccess)
            {
                getSavingDC.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }

            getSavingDC.Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
        }
    }
}
