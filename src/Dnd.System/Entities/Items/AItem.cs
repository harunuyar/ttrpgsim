namespace Dnd.System.Entities.Items;

using Dnd.System.CommandSystem.Commands;

public class AItem : IItem
{
    public AItem(IItemDescription itemDescription, int quantity = 1)
    {
        this.ItemDescription = itemDescription;
        this.Quantity = quantity;
        this.IsEquipped = false;
    }

    public string Name => ItemDescription.Name;

    public IItemDescription ItemDescription { get; }

    public int Quantity { get; private set; }

    public bool IsEquipped { get; private set; }

    public virtual void HandleCommand(ICommand command)
    {
        ItemDescription.HandleCommand(command);
    }

    public bool AddQuantity(int amount)
    {
        if (this.ItemDescription.IsStackable)
        {
            this.Quantity += amount;
            return true;
        }

        return false;
    }

    public int RemoveQuantityAndGetRemaining(int amount)
    {
        int remaining = Math.Max(0, amount - this.Quantity);
        this.Quantity = Math.Max(0, this.Quantity - amount);
        return remaining;
    }

    public void SetQuantity(int amount)
    {
        if (this.ItemDescription.IsStackable && amount > 0)
        {
            this.Quantity = amount;
        }
    }

    public bool TryEquip()
    {
        if (this.ItemDescription.IsEquippable)
        {
            this.IsEquipped = true;
            return true;
        }

        return false;
    }

    public void Unequip()
    {
        this.IsEquipped = false;
    }
}
