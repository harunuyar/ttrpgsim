namespace Dnd.System.Entities.Characters;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.Races;

public interface ICharacter : IDndEntity
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
            item.ItemDescription.HandleCommand(command);
        }

        Inventory.Equipments.Armor?.ItemDescription.HandleCommand(command);
        Inventory.Equipments.Shield?.ItemDescription.HandleCommand(command);
        Inventory.Equipments.MainHandWeapon?.ItemDescription.HandleCommand(command);
        Inventory.Equipments.OffHandWeapon?.ItemDescription.HandleCommand(command);
    }
}
