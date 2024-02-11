namespace Dnd.Entities.Items;

public class Item : IItem
{
    public Item(IItemDescription itemDescription, int quantity = 1)
    {
        this.ItemDescription = itemDescription;
        this.Quantity = quantity;
        this.IsEquipped = false;
    }

    public IItemDescription ItemDescription { get; }

    public int Quantity { get; set; }

    public bool IsEquipped { get; set; }

    public string Name => ItemDescription.Name;
}