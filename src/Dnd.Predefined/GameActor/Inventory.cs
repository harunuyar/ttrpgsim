namespace Dnd.Predefined.GameActor;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Equipment;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;
using Dnd.System.Entities.Units;

public class Inventory : IInventory
{
    public Inventory()
    {
        Wealth = new Currency();
        Items = [];
        EquipedItems = [];
    }

    public Currency Wealth { get; }

    public Dictionary<IEquipmentInstance, int> Items { get; }

    public List<IEquipmentInstance> EquipedItems { get; }

    public IEquipmentInstance? MainHandWeapon { get; private set; }

    public IEquipmentInstance? OffHandWeapon { get; private set; }

    public IEquipmentInstance? Armor { get; private set; }

    public IEquipmentInstance? Shield { get; private set; }

    public IEquipmentInstance? Ammunition { get; private set; }

    public void AddItem(IEquipmentInstance equipmentModel, int amount)
    {
        Items[equipmentModel] = Items.GetValueOrDefault(equipmentModel, 0) + amount;
    }

    public void Equip(IEquipmentInstance equipmentModel)
    {
        EquipedItems.Add(equipmentModel);
    }

    public void EquipAmmunition(IEquipmentInstance equipmentModel)
    {
        if (equipmentModel.EquipmentModel.EquipmentCategory?.Url != EquipmentCategories.Ammunition)
        {
            throw new ArgumentException("The equipment is not ammunition");
        }

        Ammunition = equipmentModel;
    }

    public void EquipArmor(IEquipmentInstance equipmentModel)
    {
        if (equipmentModel.EquipmentModel.EquipmentCategory?.Url != EquipmentCategories.Armor)
        {
            throw new ArgumentException("The equipment is not an armor");
        }

        Armor = equipmentModel;
    }

    public void EquipMainHandWeapon(IEquipmentInstance equipmentModel)
    {
        if (equipmentModel.EquipmentModel.EquipmentCategory?.Url != EquipmentCategories.Weapon)
        {
            throw new ArgumentException("The equipment is not a weapon");
        }

        MainHandWeapon = equipmentModel;

        if ((equipmentModel.EquipmentModel.Properties ?? []).Any(p => p.Url == WeaponProperties.TwoHanded))
        {
            OffHandWeapon = null;
        }
    }

    public void EquipOffHandWeapon(IEquipmentInstance equipmentModel)
    {
        if (equipmentModel.EquipmentModel.EquipmentCategory?.Url != EquipmentCategories.Weapon)
        {
            throw new ArgumentException("The equipment is not a weapon");
        }

        if ((MainHandWeapon?.EquipmentModel?.Properties ?? []).Any(p => p.Url == WeaponProperties.TwoHanded))
        {
            MainHandWeapon = null;
        }

        if ((equipmentModel.EquipmentModel.Properties ?? []).Any(p => p.Url == WeaponProperties.TwoHanded))
        {
            MainHandWeapon = equipmentModel;
        }
        else
        {
            OffHandWeapon = equipmentModel;
        }
    }

    public void EquipShield(IEquipmentInstance equipmentModel)
    {
        if (equipmentModel.EquipmentModel.ArmorCategory != EArmorCategory.Shield)
        {
            throw new ArgumentException("The equipment is not a shield");
        }

        Shield = equipmentModel;
    }

    public int GetQuantity(IEquipmentInstance equipmentModel)
    {
        return Items.GetValueOrDefault(equipmentModel, 0);
    }

    public void RemoveItem(IEquipmentInstance equipmentModel, int amount)
    {
        Items[equipmentModel] -= amount;
        if (Items[equipmentModel] <= 0)
        {
            Items.Remove(equipmentModel);

            EquipedItems.Remove(equipmentModel);

            if (MainHandWeapon == equipmentModel)
            {
                MainHandWeapon = null;
            }

            if (OffHandWeapon == equipmentModel)
            {
                OffHandWeapon = null;
            }

            if (Armor == equipmentModel)
            {
                Armor = null;
            }

            if (Shield == equipmentModel)
            {
                Shield = null;
            }

            if (Ammunition == equipmentModel)
            {
                Ammunition = null;
            }
        }
    }

    public void Unequip(IEquipmentInstance equipmentModel)
    {
        EquipedItems.Remove(equipmentModel);
    }

    public void UnequipAmmunition()
    {
        Ammunition = null;
    }

    public void UnequipArmor()
    {
        Armor = null;
    }

    public void UnequipMainHandWeapon()
    {
        MainHandWeapon = null;
    }

    public void UnequipOffHandWeapon()
    {
        OffHandWeapon = null;
    }

    public void UnequipShield()
    {
        Shield = null;
    }

    public async Task HandleCommand(ICommand command)
    {
        if (MainHandWeapon is not null)
        {
            await MainHandWeapon.HandleCommand(command);
        }

        if (OffHandWeapon is not null)
        {
            await OffHandWeapon.HandleCommand(command);
        }

        if (Armor is not null)
        {
            await Armor.HandleCommand(command);
        }

        if (Shield is not null)
        {
            await Shield.HandleCommand(command);
        }

        if (Ammunition is not null)
        {
            await Ammunition.HandleCommand(command);
        }

        foreach (var item in EquipedItems)
        {
            await item.HandleCommand(command);
        }
    }
}
