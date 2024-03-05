namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Instances;

public interface IWeaponAttackAction : IAttackRollAction
{
    IEquipmentInstance Weapon { get; }
    EAttackHandType HandType { get; }
}
