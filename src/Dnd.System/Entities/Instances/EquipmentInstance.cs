namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Equipment;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;

public class EquipmentInstance : IUsageBonusProvider
{
    public EquipmentInstance(EquipmentModel equipmentModel)
    {
        EquipmentModel = equipmentModel;
    }

    public EquipmentModel EquipmentModel { get; }

    public virtual Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public override bool Equals(object? obj)
    {
        return obj is EquipmentInstance equipmentInstance
            && equipmentInstance.EquipmentModel == EquipmentModel;
    }

    public override int GetHashCode()
    {
        return EquipmentModel.GetHashCode();
    }
}
