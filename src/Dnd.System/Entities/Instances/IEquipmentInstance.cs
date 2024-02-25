namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Equipment;

public interface IEquipmentInstance : ICommandHandler, IUsageBonusProvider
{
    EquipmentModel EquipmentModel { get; }
}
