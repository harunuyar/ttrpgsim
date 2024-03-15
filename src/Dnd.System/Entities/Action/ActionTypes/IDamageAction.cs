namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.DamageType;

public interface IDamageAction : IAmountAction
{
    DamageTypeModel DamageType { get; }
}
