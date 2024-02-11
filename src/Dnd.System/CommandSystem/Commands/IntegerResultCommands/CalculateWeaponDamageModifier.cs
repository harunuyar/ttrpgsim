namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CalculateWeaponDamageModifier : DndScoreCommand
{
    public CalculateWeaponDamageModifier(ICharacter character, IItem weaponItem) : base(character)
    {
        WeaponItem = weaponItem;
    }

    public IItem WeaponItem { get; }

    public override void InitializeResult()
    {
        IWeapon? weapon = WeaponItem.ItemDescription as IWeapon;
        if (weapon == null)
        {
            Result.SetError("Item is not a weapon");
            return;
        }

        var getStrengthModifier = new GetAttributeModifier(this.Character, EAttributeType.Strength);
        var strengthModifier = getStrengthModifier.Execute();

        if (strengthModifier.IsSuccess)
        {
            IAttribute usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Strength);
            int attributeModifier = strengthModifier.Value;

            if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
            {
                var getDexterityModifier = new GetAttributeModifier(this.Character, EAttributeType.Dexterity);
                var dexterityModifier = getDexterityModifier.Execute();

                if (dexterityModifier.IsSuccess && dexterityModifier.Value > attributeModifier)
                {
                    attributeModifier = dexterityModifier.Value;
                    usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Dexterity);
                }
            }

            if (WeaponItem == Character.Inventory.Equipments.MainHandWeapon || attributeModifier < 0)
            {
                Result.SetBaseValue(usedAttribute, attributeModifier);
            }
            else
            {
                // don't add the attribute modifier if it is an off-hand weapon attack and the modifier a positive value
                Result.SetBaseValue("Base", 0);
            }
        }
        else
        {
            Result.SetError(strengthModifier.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }
}
