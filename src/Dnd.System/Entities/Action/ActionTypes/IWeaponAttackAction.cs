namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Instances;

public interface IWeaponAttackAction : IAttackAction
{
    IEquipmentInstance Weapon { get; }
}
