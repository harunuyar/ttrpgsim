namespace Dnd.CommandSystem.Commands;

using Dnd.CommandSystem.Results;
using Dnd.Entities.Characters;

public abstract class DndCommand : ICommand
{
    public DndCommand(Character character)
    {
        Character = character;
    }

    public Character Character { get; }

    public abstract ICommandResult Execute();
    
    protected void CollectBonuses()
    {
        foreach (var trait in Character.Traits)
        {
            trait.HandleCommand(this);
        }

        foreach (var feat in Character.Feats)
        {
            feat.HandleCommand(this);
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
        Character.Inventory.Equipments.RangedWeapon?.ItemDescription.HandleCommand(this);
    }
}
