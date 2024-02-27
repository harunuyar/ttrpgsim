namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Common;

public interface IAreaOfEffectAction : IEffectAction, ITargetingAction
{
    AreaOfEffectModel AreaOfEffect { get; }
}
