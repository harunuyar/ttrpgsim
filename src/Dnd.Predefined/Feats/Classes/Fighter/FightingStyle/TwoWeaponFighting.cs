namespace Dnd.Predefined.Feats.Classes.Fighter.FightingStyle;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.Impl;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class TwoWeaponFighting : AFeat, IFightingStyle
{
    public TwoWeaponFighting() : base("Two-Weapon Fighting", "When you engage in two-weapon fighting, you can add your ability modifier to the damage of the second attack.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetDamageModifier getDamageModifier)
        {
            if (getDamageModifier.AttackAction is WeaponAttack weaponAttack && getDamageModifier.AttackAction.ActionType == EActionType.BonusAction)
            {
                var strengthModifier = new GetAttributeModifier(command.Actor, EAttributeType.Strength).Execute();

                if (!strengthModifier.IsSuccess)
                {
                    getDamageModifier.SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
                    return;
                }

                int attributeModifier = strengthModifier.Value;

                if (weaponAttack.Weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
                {
                    var dexterityModifier = new GetAttributeModifier(command.Actor, EAttributeType.Dexterity).Execute();

                    if (!dexterityModifier.IsSuccess)
                    {
                        getDamageModifier.SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
                        return;
                    }

                    if (dexterityModifier.Value > attributeModifier)
                    {
                        attributeModifier = dexterityModifier.Value;
                    }
                }

                if (attributeModifier > 0) // it is already added if it is a negative value
                {
                    getDamageModifier.AddBonus(this, attributeModifier);
                }
            }
        }
    }
}
