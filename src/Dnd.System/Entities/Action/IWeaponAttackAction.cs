namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.Instances;

public interface IWeaponAttackAction : IAttackAction
{
    EquipmentInstance Weapon { get; }
}
