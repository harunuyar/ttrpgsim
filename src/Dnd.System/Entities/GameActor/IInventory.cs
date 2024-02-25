namespace Dnd.System.Entities.GameActor;

using Dnd.System.Entities.Instances;
using Dnd.System.Entities.Units;

public interface IInventory : ICommandHandler
{
    Currency Wealth { get; }
    Dictionary<IEquipmentInstance, int> Items { get; }
    List<IEquipmentInstance> EquipedItems { get; }
    void AddItem(IEquipmentInstance equipmentModel, int amount);
    void RemoveItem(IEquipmentInstance equipmentModel, int amount);
    int GetQuantity(IEquipmentInstance equipmentModel);
    void Equip(IEquipmentInstance equipmentModel);
    void Unequip(IEquipmentInstance equipmentModel);
    void EquipMainHandWeapon(IEquipmentInstance equipmentModel);
    void UnequipMainHandWeapon();
    void EquipOffHandWeapon(IEquipmentInstance equipmentModel);
    void UnequipOffHandWeapon();
    void EquipArmor(IEquipmentInstance equipmentModel);
    void UnequipArmor();
    void EquipShield(IEquipmentInstance equipmentModel);
    void UnequipShield();
    void EquipAmmunition(IEquipmentInstance equipmentModel);
    void UnequipAmmunition();
}
