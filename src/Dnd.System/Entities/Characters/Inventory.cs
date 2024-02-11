namespace Dnd.Entities.Characters;

using Dnd.Entities.Items;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
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
        if (item.ItemDescription is AArmor)
        {
            EquipArmor(item);
        }
        else if (item.ItemDescription is AWeapon)
        {
            EquipWeapon(item, true);
        }
        else if (item.ItemDescription is Shield)
        {
            EquipShield(item);
        }
        else
        {
            if (item.TryEquip())
            {
                Items.Add(item);
                Equipments.EquipedItems.Add(item);
            }
        }
    }

    public void UnequipItem(IItem item)
    {
        if (item.ItemDescription is AArmor)
        {
            UnequipArmor();
        }
        else if (item.ItemDescription is AWeapon)
        {
            UnequipWeapon(true);
        }
        else if (item.ItemDescription is Shield)
        {
            UnequipShield();
        }
        else
        {
            item.Unequip();
            Equipments.EquipedItems.Remove(item);
        }
    }

    public void EquipArmor(IItem item)
    {
        UnequipArmor();
        if (item.TryEquip())
        {
            Items.Add(item);
            Equipments.Armor = item;
        }
    }

    public void UnequipArmor()
    {
        if (Equipments.Armor != null)
        {
            Equipments.Armor.Unequip();
            Equipments.Armor = null;
        }
    }

    public void EquipShield(IItem item)
    {
        UnequipShield();
        if (item.TryEquip())
        {
            Items.Add(item);
            Equipments.Shield = item;
        }
    }

    public void UnequipShield()
    {
        if (Equipments.Shield != null)
        {
            Equipments.Shield.Unequip();
            Equipments.Shield = null;
        }
    }

    public void EquipWeapon(IItem item, bool mainHand)
    {
        UnequipWeapon(mainHand);

        if (item.ItemDescription is AWeapon weapon && weapon.WeaponProperties.HasFlag(EWeaponProperty.TwoHanded)) // If the weapon is two-handed
        {
            UnequipWeapon(!mainHand); // Unequip the other-hand weapon
            mainHand = true; // Equip the two-handed weapon in the main hand
        }
        else if (Equipments.MainHandWeapon?.ItemDescription is AWeapon mainHandWeapon && mainHandWeapon.WeaponProperties.HasFlag(EWeaponProperty.TwoHanded)) // If the main hand weapon is two-handed
        {
            UnequipWeapon(true); // Unequip the two-handed weapon
        }
        
        if (item.TryEquip())
        {
            Items.Add(item);

            if (mainHand)
            {
                Equipments.MainHandWeapon = item;
            }
            else
            {
                Equipments.OffHandWeapon = item;
            }
            
            Equipments.MainHandWeapon = item;
        }
    }

    public void UnequipWeapon(bool mainHand)
    {
        if (mainHand)
        {
            if (Equipments.MainHandWeapon != null)
            {
                Equipments.MainHandWeapon.Unequip();
                Equipments.MainHandWeapon = null;
            }
        }
        else
        {
            if (Equipments.OffHandWeapon != null)
            {
                Equipments.OffHandWeapon.Unequip();
                Equipments.OffHandWeapon = null;
            }
        }
    }
}
