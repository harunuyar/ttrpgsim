namespace Dnd.System.Entities.Items;

public interface IItem : IDndEntity
{
    IItemDescription ItemDescription { get; }

    int Quantity { get; protected set; }

    bool IsEquipped { get; protected set; }

    bool AddQuantity(int amount)
    {
        if (this.ItemDescription.IsStackable)
        {
            this.Quantity += amount;
            return true;
        }

        return false;
    }

    int RemoveQuantityAndGetRemaining(int amount)
    {
        int remaining = Math.Max(0, amount - this.Quantity);
        this.Quantity = Math.Max(0, this.Quantity - amount);
        return remaining;
    }

    void SetQuantity(int amount)
    {
        if (this.ItemDescription.IsStackable && amount > 0)
        {
            this.Quantity = amount;
        }
    }

    bool TryEquip()
    {
        if (this.ItemDescription.IsEquippable)
        {
            this.IsEquipped = true;
            return true;
        }

        return false;
    }

    void Unequip()
    {
        this.IsEquipped = false;
    }
}
