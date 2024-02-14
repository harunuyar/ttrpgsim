namespace Dnd.System.Entities.Items;

public interface IItem : IBonusProvider
{
    IItemDescription ItemDescription { get; }

    int Quantity { get; }

    bool IsEquipped { get; }

    bool AddQuantity(int amount);

    int RemoveQuantityAndGetRemaining(int amount);

    void SetQuantity(int amount);

    bool TryEquip();

    void Unequip();
}
