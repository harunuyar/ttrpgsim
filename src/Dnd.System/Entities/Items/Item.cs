namespace Dnd.System.Entities.Items;

public class Item : AItem
{
    public Item(IItemDescription itemDescription, int quantity = 1) : base(itemDescription, quantity)
    {
    }
}