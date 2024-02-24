namespace Dnd.System.Entities.GameActor;

using Dnd.System.Entities.Instances;
using Dnd.System.Entities.Units;

public class Inventory
{
    public Inventory()
    {
        this.Wealth = new Value();
        this.EquipedItems = [];
        this.Items = [];
    }

    public Value Wealth { get; set; }

    public Dictionary<EquipmentInstance, int> Items { get; }

    public List<EquipmentInstance> EquipedItems { get; set; }

    public void AddItem(EquipmentInstance equipmentModel, int amount)
    {
        if (Items.ContainsKey(equipmentModel))
        {
            Items[equipmentModel] += amount;
        }
        else
        {
            Items[equipmentModel] = amount;
        }
    }

    public void RemoveItem(EquipmentInstance equipmentModel, int amount)
    {
        if (Items.ContainsKey(equipmentModel))
        {
            Items[equipmentModel] -= amount;
            if (Items[equipmentModel] <= 0)
            {
                Items.Remove(equipmentModel);
            }
        }
    }

    public int GetQuantity(EquipmentInstance equipmentModel)
    {
        return Items.GetValueOrDefault(equipmentModel, 0);
    }

    public void Equip(EquipmentInstance equipmentModel)
    {
        EquipedItems.Add(equipmentModel);
    }

    public void Unequip(EquipmentInstance equipmentModel)
    {
        EquipedItems.Remove(equipmentModel);
    }
}
