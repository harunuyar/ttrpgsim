namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Instances;

public interface IImprovisedAttack : IAttackRollAction
{
    IEquipmentInstance? ImprovisedWeapon { get; }
}
