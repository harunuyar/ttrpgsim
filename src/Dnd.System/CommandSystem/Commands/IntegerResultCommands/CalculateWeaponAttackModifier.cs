namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CalculateWeaponAttackModifier : DndScoreCommand
{
    public CalculateWeaponAttackModifier(IGameActor character, IItem weaponItem, IGameActor target) : base(character)
    {
        this.WeaponItem = weaponItem;
        this.Target = target;
    }

    public IItem WeaponItem { get; }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
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

        var getStrengthModifier = new GetAttributeModifier(this.Actor, EAttributeType.Strength);
        var strengthModifier = getStrengthModifier.Execute();

        if (!strengthModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
            return;
        }

        IAttribute usedAttribute = this.Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
        int attributeModifier = strengthModifier.Value;

        if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
        {
            var getDexterityModifier = new GetAttributeModifier(this.Actor, EAttributeType.Dexterity);
            var dexterityModifier = getDexterityModifier.Execute();

            if (!dexterityModifier.IsSuccess)
            {
                SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
                return;
            }

            if (dexterityModifier.Value > attributeModifier)
            {
                attributeModifier = dexterityModifier.Value;
                usedAttribute = this.Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
            }
        }

        Result.SetBaseValue(usedAttribute, attributeModifier);
    }
}
