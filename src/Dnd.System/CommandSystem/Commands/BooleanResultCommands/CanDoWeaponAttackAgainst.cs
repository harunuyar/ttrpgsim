namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class CanDoWeaponAttackAgainst : DndBooleanCommand
{
    public CanDoWeaponAttackAgainst(IGameActor character, IItem weaponItem, IGameActor attacker) : base(character)
    {
        Attacker = attacker;
        WeaponItem = weaponItem;
    }

    public IGameActor Attacker { get; }

    public IItem WeaponItem { get; }

    protected override void InitializeResult()
    {
        if (WeaponItem.ItemDescription is not IWeapon)
        {
            SetErrorAndReturn("The item is not a weapon.");
            return;
        }

        SetValue(true, "By default, anyone can do a weapon attack on you.");
    }
}
