namespace Dnd.System.Entities.Instances;

public interface IMagicEquipmentInstance : IEquipmentInstance
{
    List<IMagicItemInstance> MagicItems { get; }
}
