namespace Dnd.System.Entities.Effect;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public interface IEffectDefinition
{
    string Name { get; }
    string Description { get; }
    Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner);
}
