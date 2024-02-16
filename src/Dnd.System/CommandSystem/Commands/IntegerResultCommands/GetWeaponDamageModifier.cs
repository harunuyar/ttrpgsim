namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetWeaponDamageModifier : DndScoreCommand
{
    public GetWeaponDamageModifier(IGameActor character, IItem weaponItem, IGameActor target) : base(character)
    {
        WeaponItem = weaponItem;
        Target = target;
    }

    public IItem WeaponItem { get; }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
        IWeapon? weapon = WeaponItem.ItemDescription as IWeapon;
        if (weapon == null)
        {
            SetErrorAndReturn("Item is not a weapon");
            return;
        }

        Result.SetBaseValue("Base", 0);

        var strengthModifier = new GetAttributeModifier(this.Actor, EAttributeType.Strength).Execute();

        if (!strengthModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
            return;
        }

        IAttribute usedAttribute = this.Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
        var attributeModifier = strengthModifier;

        if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
        {
            var dexterityModifier = new GetAttributeModifier(this.Actor, EAttributeType.Dexterity).Execute();

            if (!dexterityModifier.IsSuccess)
            {
                SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
                return;
            }

            if (dexterityModifier.Value > attributeModifier.Value)
            {
                attributeModifier = dexterityModifier;
                usedAttribute = this.Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
            }
        }

        if (WeaponItem == Actor.Inventory.Equipments.MainHandWeapon || attributeModifier.Value < 0)
        {
            Result.AddAsBonus(usedAttribute, attributeModifier);
        }
    }
}
