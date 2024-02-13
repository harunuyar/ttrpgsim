namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CanDoWeaponAttack : DndBooleanCommand
{
    public CanDoWeaponAttack(IGameActor character, IItem weaponItem, IGameActor target) : base(character)
    {
        WeaponItem = weaponItem;
        Target = target;
    }

    public IItem WeaponItem { get; set; }

    public IGameActor Target { get; set; }

    protected override void InitializeResult()
    {
        if (WeaponItem.ItemDescription is not IWeapon)
        {
            Result.SetError("The item is not a weapon.");
        }

        Result.SetValue("Default", true);
    }

    protected override void FinalizeResult()
    {
    }
}
