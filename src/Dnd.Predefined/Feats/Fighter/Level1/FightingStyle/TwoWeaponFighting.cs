namespace Dnd.Predefined.Feats.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class TwoWeaponFighting : AFeat, IFightingStyle
{
    public TwoWeaponFighting() : base("Two-Weapon Fighting", "When you engage in two-weapon fighting, you can add your ability modifier to the damage of the second attack.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        if (command is CalculateWeaponDamageModifier calculateWeaponDamageModifier 
            && calculateWeaponDamageModifier.WeaponItem.ItemDescription is IWeapon weapon
            && calculateWeaponDamageModifier.WeaponItem == command.Character.Inventory.Equipments.OffHandWeapon)
        {
            var getStrengthModifier = new GetAttributeModifier(command.Character, EAttributeType.Strength);
            var strengthModifier = getStrengthModifier.Execute();

            if (strengthModifier.IsSuccess)
            {
                IAttribute usedAttribute = command.Character.AttributeSet.GetAttribute(EAttributeType.Strength);
                int attributeModifier = strengthModifier.Value;

                if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
                {
                    var getDexterityModifier = new GetAttributeModifier(command.Character, EAttributeType.Dexterity);
                    var dexterityModifier = getDexterityModifier.Execute();

                    if (dexterityModifier.IsSuccess && dexterityModifier.Value > attributeModifier)
                    {
                        attributeModifier = dexterityModifier.Value;
                        usedAttribute = command.Character.AttributeSet.GetAttribute(EAttributeType.Dexterity);
                    }
                }

                calculateWeaponDamageModifier.Result.SetBaseValue(usedAttribute, attributeModifier);
            }
            else
            {
                calculateWeaponDamageModifier.Result.SetError(strengthModifier.ErrorMessage ?? "Couldn't get attribute modifier");
            }
        }
    }
}
