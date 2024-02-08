namespace DnD.Entities.Characters;

using DnD.Entities.Items;
using DnD.Entities.Units;
using System.Collections.Generic;

internal class Inventory
{
    public Inventory()
    {
        this.Wealth = new Worth();
        this.Items = new List<IItem>();
    }

    public Worth Wealth { get; set; }

    public List<IItem> Items { get; }

    public void AddItem(IItem item)
    {
        AddItem(item.ItemDescription, item.Quantity);
    }

    public void AddItem(IItemDescription itemDescription, int amount)
    {
        if (itemDescription.IsStackable)
        {
            IItem? item = this.Items.Find(i => i.ItemDescription == itemDescription);
            if (item != null)
            {
                item.Quantity += amount;
                return;
            }
        }
            
        IItem newItem = new Item(itemDescription);
        newItem.Quantity = amount;
        this.Items.Add(newItem);
    }

    public void RemoveItem(IItem item, int amount)
    {
        if (Items.Contains(item))
        {
            int toRemove = Math.Min(item.Quantity, amount);
            
            item.Quantity -= toRemove;
            amount -= toRemove;

            if (item.Quantity == 0)
            {
                this.Items.Remove(item);
            }

            if (amount > 0) 
            { 
                RemoveItem(item.ItemDescription, amount);
            }
        }
        else
        {
            RemoveItem(item.ItemDescription, amount);
        }
    }

    public void RemoveItem(IItemDescription itemDescription, int amount)
    {
        while (amount > 0)
        {
            IItem? item = this.Items.Find(i => i.ItemDescription == itemDescription);
            if (item != null)
            {
                int toRemove = Math.Min(item.Quantity, amount);
                
                item.Quantity -= toRemove;
                amount -= toRemove;

                if (item.Quantity == 0)
                {
                    this.Items.Remove(item);
                }
            }
            else
            {
                amount = 0;
            }
        }
    }
}
