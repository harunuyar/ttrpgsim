namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class TwoWeaponFighting : AFeat, IFightingStyle
{
    public TwoWeaponFighting() : base("Two-Weapon Fighting", "When you engage in two-weapon fighting, you can add your ability modifier to the damage of the second attack.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetWeaponDamageModifier calculateWeaponDamageModifier
            && calculateWeaponDamageModifier.WeaponItem.ItemDescription is IWeapon weapon
            && calculateWeaponDamageModifier.WeaponItem == command.Actor.Inventory.Equipments.OffHandWeapon)
        {
            var getStrengthModifier = new GetAttributeModifier(command.Actor, EAttributeType.Strength);
            var strengthModifier = getStrengthModifier.Execute();

            if (strengthModifier.IsSuccess)
            {
                int attributeModifier = strengthModifier.Value;

                if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
                {
                    var getDexterityModifier = new GetAttributeModifier(command.Actor, EAttributeType.Dexterity);
                    var dexterityModifier = getDexterityModifier.Execute();

                    if (dexterityModifier.IsSuccess)
                    {
                        if (dexterityModifier.Value > attributeModifier)
                        {
                            attributeModifier = dexterityModifier.Value;
                        }
                    }
                    else
                    {
                        calculateWeaponDamageModifier.SetErrorAndReturn("Couldn't get dexterity modifier: " + strengthModifier.ErrorMessage);
                    }
                }

                calculateWeaponDamageModifier.SetBaseValue(this, attributeModifier);
            }
            else
            {
                calculateWeaponDamageModifier.SetErrorAndReturn("Couldn't get strength modifier: " + strengthModifier.ErrorMessage);
            }
        }
    }
}
