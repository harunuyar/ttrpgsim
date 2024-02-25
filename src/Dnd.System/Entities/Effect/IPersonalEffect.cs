namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.GameActor;

public interface IPersonalEffect : IEffect
{
    IGameActor Target { get; }
}
