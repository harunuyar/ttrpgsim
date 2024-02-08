namespace DnD.Entities.Items;

internal class Item : IItem
{
    public Item(IItemDescription itemDescription)
    {
        this.ItemDescription = itemDescription;
        this.Quantity = 1;
        this.IsEquipped = false;
    }

    public IItemDescription ItemDescription { get; }
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; }
}