namespace Dnd.System.Entities.GameActors;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.Races;

public interface IGameActor : IDndEntity
{
    IRace Race { get; }

    IAlignment Alignment { get; }

    AttributeSet AttributeSet { get; }

    LevelInfo LevelInfo { get; }

    HitPoints HitPoints { get; }

    Inventory Inventory { get; }

    EffectsTable EffectsTable { get; }

    bool HasInspiration { get; set; }

    internal void HandleCommand(ICommand command)
    {
        foreach (var trait in Race.RaceTraits)
        {
            trait.HandleCommand(command);
        }

        LevelInfo.HandleCommand(command);

        EffectsTable.HandleCommand(command);

        foreach (var item in Inventory.Equipments.EquipedItems)
        {
            item.HandleCommand(command);
        }

        Inventory.Equipments.Armor?.HandleCommand(command);
        Inventory.Equipments.Shield?.HandleCommand(command);
        Inventory.Equipments.MainHandWeapon?.HandleCommand(command);
        Inventory.Equipments.OffHandWeapon?.HandleCommand(command);
    }
}
