namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Common;

public interface IAreaOfEffectAction : IAction
{
    AreaOfEffectModel AreaOfEffect { get; }
}
