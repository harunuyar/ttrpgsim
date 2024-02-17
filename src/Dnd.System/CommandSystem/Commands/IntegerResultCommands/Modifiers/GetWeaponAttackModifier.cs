namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetWeaponAttackModifier : GetAttackModifier
{
    public GetWeaponAttackModifier(IGameActor character, IItem weaponItem, IGameActor? target) : base(character, target)
    {
        WeaponItem = weaponItem;
    }

    public IItem WeaponItem { get; }

    protected override void InitializeResult()
    {
        base.InitializeResult();

        if (WeaponItem.ItemDescription is not IWeapon weapon)
        {
            SetErrorAndReturn("Item is not a weapon");
            return;
        }

        if (weapon.SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
        {
            SetErrorAndReturn("Weapon doesn't use attack roll");
            return;
        }

        var strengthModifier = new GetAttributeModifier(Actor, EAttributeType.Strength).Execute();

        if (!strengthModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
            return;
        }

        IAttribute usedAttribute = Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
        var attributeModifier = strengthModifier;

        if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
        {
            var getDexterityModifier = new GetAttributeModifier(Actor, EAttributeType.Dexterity);
            var dexterityModifier = getDexterityModifier.Execute();

            if (!dexterityModifier.IsSuccess)
            {
                SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
                return;
            }

            if (dexterityModifier.Value > attributeModifier.Value)
            {
                attributeModifier = dexterityModifier;
                usedAttribute = Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
            }
        }

        Result.AddAsBonus(usedAttribute, attributeModifier);

        var hasProficiency = new HasWeaponProficiency(Actor, weapon.WeaponType).Execute();

        if (!hasProficiency.IsSuccess)
        {
            SetErrorAndReturn("HasWeaponProficiency: " + hasProficiency.ErrorMessage);
            return;
        }

        if (hasProficiency.Value)
        {
            var proficiencyBonus = new GetProficiencyBonus(Actor).Execute();

            if (!proficiencyBonus.IsSuccess)
            {
                SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
        }

        if (Target != null)
        {
            var attackModifierAgainst = new GetWeaponAttackModifierAgainst(Target, WeaponItem, Actor).Execute();

            if (!attackModifierAgainst.IsSuccess)
            {
                SetErrorAndReturn("GetWeaponAttackModifierAgainst: " + attackModifierAgainst.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Attack Modifier From Target", attackModifierAgainst);
        }
    }
}
