namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class CalculateWeaponDamageModifier : DndScoreCommand
{
    public CalculateWeaponDamageModifier(Character character, AWeapon weapon) : base(character)
    {
        Weapon = weapon;
    }

    public AWeapon Weapon { get; }

    public override void InitializeResult()
    {
        var getStrengthModifier = new GetAttributeModifier(this.Character, EAttributeType.Strength);
        var strengthModifier = getStrengthModifier.Execute();

        if (strengthModifier.IsSuccess)
        {
            IAttribute usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Strength);
            int attributeModifier = strengthModifier.Value;

            if (Weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
            {
                var getDexterityModifier = new GetAttributeModifier(this.Character, EAttributeType.Dexterity);
                var dexterityModifier = getDexterityModifier.Execute();

                if (dexterityModifier.IsSuccess && dexterityModifier.Value > attributeModifier)
                {
                    attributeModifier = dexterityModifier.Value;
                    usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Dexterity);
                }
            }

            if (Weapon == Character.Inventory.Equipments.MainHandWeapon || attributeModifier < 0)
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
