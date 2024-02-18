namespace Dnd.System.Entities.GameActors;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;
using Dnd.System.Entities.Actions.Impl;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class Equipments : IBonusProvider
{
    public Equipments()
    {
        this.EquipedItems = new HashSet<IItem>();
    }

    public IItem? Armor { get; set; }

    public IItem? Shield { get; set; }

    public IItem? MainHandWeapon { get; set; }

    public IItem? OffHandWeapon { get; set; }

    public HashSet<IItem> EquipedItems { get; }

    public string Name => throw new NotImplementedException();

    public void HandleCommand(ICommand command)
    {
        if (command is GetActions getPossibleActions)
        {
            if (MainHandWeapon != null && MainHandWeapon.ItemDescription is IWeapon weapon1)
            {
                getPossibleActions.AddItem(this, new WeaponAttack(weapon1, Actions.EActionType.MainAction, false));
                getPossibleActions.AddItem(this, new WeaponAttack(weapon1, Actions.EActionType.Reaction, false));

                if (OffHandWeapon == null && Shield == null && weapon1.WeaponProperties.HasFlag(EWeaponProperty.Versatile))
                {
                    getPossibleActions.AddItem(this, new WeaponAttack(weapon1, Actions.EActionType.MainAction, true));
                }
            }

            if (OffHandWeapon != null && OffHandWeapon.ItemDescription is IWeapon weapon2)
            {
                getPossibleActions.AddItem(this, new WeaponAttack(weapon2, Actions.EActionType.BonusAction, false));
            }
        }
    }
}
