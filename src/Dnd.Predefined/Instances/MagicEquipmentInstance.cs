namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Equipment;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class MagicEquipmentInstance : EquipmentInstance, IMagicEquipmentInstance
{
    public MagicEquipmentInstance(EquipmentModel equipmentModel, IEnumerable<IMagicItemInstance> magicItems) : base(equipmentModel)
    {
        MagicItems = magicItems.ToList();
    }

    public List<IMagicItemInstance> MagicItems { get; }

    public override Task HandleCommand(ICommand command)
    {
        foreach (var magicItem in MagicItems)
        {
            magicItem.HandleCommand(command);
        }

        return base.HandleCommand(command);
    }

    public override Task HandleUsageCommand(ICommand command)
    {
        foreach (var magicItem in MagicItems)
        {
            magicItem.HandleUsageCommand(command);
        }

        return base.HandleUsageCommand(command);
    }

    public override bool Equals(object? obj)
    {
        return obj is MagicEquipmentInstance magicEquipmentInstance
            && magicEquipmentInstance.EquipmentModel == EquipmentModel
            && magicEquipmentInstance.MagicItems.Equals(MagicItems);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(EquipmentModel, MagicItems);
    }
}
