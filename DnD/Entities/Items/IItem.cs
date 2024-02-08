namespace DnD.Entities.Items;

internal interface IItem
{
    IItemDescription ItemDescription { get; }
    int Quantity { get; set; }
    bool IsEquipped { get; set; }
}
