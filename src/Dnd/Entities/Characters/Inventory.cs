namespace Dnd.Entities.Characters;

using Dnd.Entities.Items;
using Dnd.Entities.Units;
using System.Collections.Generic;

public class Inventory
{
    public Inventory()
    {
        this.Equipments = new Equipments();
        this.Wealth = new Value();
        this.Items = new HashSet<IItem>();
    }

    public Equipments Equipments { get; set; }

    public Value Wealth { get; set; }

    public HashSet<IItem> Items { get; }

    public void AddItem(IItem item)
    {
        AddItem(item.ItemDescription, item.Quantity);
    }

    public void AddItem(IItemDescription itemDescription, int amount)
    {
        if (itemDescription.IsStackable)
        {
            IItem? item = this.Items.FirstOrDefault(i => i.ItemDescription == itemDescription);
            if (item != null)
            {
                item.AddQuantity(amount);
                return;
            }
            else
            {
                IItem newItem = new Item(itemDescription, amount);
                this.Items.Add(newItem);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                IItem newItem = new Item(itemDescription);
                this.Items.Add(newItem);
            }
        }
    }

    public void RemoveItem(IItem item, int amount)
    {
        if (Items.Contains(item))
        {
            amount = item.RemoveQuantityAndGetRemaining(amount);

            if (item.Quantity == 0)
            {
                this.Items.Remove(item);
            }
        }

        if (amount > 0)
        {
            RemoveItem(item.ItemDescription, amount);
        }
    }

    public void RemoveItem(IItemDescription itemDescription, int amount)
    {
        while (amount > 0)
        {
            IItem? item = this.Items.FirstOrDefault(i => i.ItemDescription == itemDescription);
            if (item != null)
            {
                amount = item.RemoveQuantityAndGetRemaining(amount);

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

    public int GetQuantity(IItemDescription itemDescription)
    {
        return this.Items
            .Where(i => i.ItemDescription == itemDescription)
            .Select(i => i.Quantity)
            .Sum();
    }

    public void EquipItem(IItem item)
    {
        if (item.TryEquip())
        {
            Items.Add(item);
            Equipments.EquipedItems.Add(item);
        }
    }

    public void UnequipItem(IItem item)
    {
        item.Unequip();
        Equipments.EquipedItems.Remove(item);
    }
}
