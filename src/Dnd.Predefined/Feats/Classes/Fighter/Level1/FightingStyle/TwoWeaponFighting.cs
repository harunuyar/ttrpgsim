namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.Impl;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class TwoWeaponFighting : AFeat, IFightingStyle
{
    public TwoWeaponFighting() : base("Two-Weapon Fighting", "When you engage in two-weapon fighting, you can add your ability modifier to the damage of the second attack.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetDamageModifier getDamageModifier
            && getDamageModifier.AttackAction is WeaponAttack weaponAttack
            && getDamageModifier.AttackAction.ActionType == EActionType.BonusAction)
        {
            var getStrengthModifier = new GetAttributeModifier(command.Actor, EAttributeType.Strength);
            var strengthModifier = getStrengthModifier.Execute();

            if (strengthModifier.IsSuccess)
            {
                int attributeModifier = strengthModifier.Value;

                if (weaponAttack.Weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
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
                        getDamageModifier.SetErrorAndReturn("Couldn't get dexterity modifier: " + strengthModifier.ErrorMessage);
                    }
                }

                getDamageModifier.SetBaseValue(this, attributeModifier);
            }
            else
            {
                getDamageModifier.SetErrorAndReturn("Couldn't get strength modifier: " + strengthModifier.ErrorMessage);
            }
        }
    }
}
