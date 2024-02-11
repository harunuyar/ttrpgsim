namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public abstract class DndCommand : ICommand
{
    public DndCommand(ICharacter character)
    {
        Character = character;
    }

    public ICharacter Character { get; }

    public abstract ICommandResult Execute();
    
    protected void CollectBonuses()
    {
        foreach (var trait in Character.Race.RaceTraits)
        {
            trait.HandleCommand(this);
        }

        foreach (var effect in Character.Effects)
        {
            effect.HandleCommand(this);
        }

        foreach (var item in Character.Inventory.Equipments.EquipedItems)
        {
            item.ItemDescription.HandleCommand(this);
        }

        Character.Inventory.Equipments.Armor?.ItemDescription.HandleCommand(this);
        Character.Inventory.Equipments.Shield?.ItemDescription.HandleCommand(this);
        Character.Inventory.Equipments.MainHandWeapon?.ItemDescription.HandleCommand(this);
        Character.Inventory.Equipments.OffHandWeapon?.ItemDescription.HandleCommand(this);
    }
}
