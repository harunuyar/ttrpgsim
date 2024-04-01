namespace Dnd.Predefined.Effects;

using Dnd.Predefined.Commands.ListCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class ActiveEffectDefinition : IActiveEffectDefinition
{
    public ActiveEffectDefinition(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }

    public string Description { get; }

    public abstract Task<bool> ShouldActivate(IGameActor source, IGameActor target, IEvent eventToReactTo);

    public abstract Task<IEvent> CreateEvent(IGameActor source, IGameActor target, IEvent eventToReactTo);

    public async Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is GetReactionEvents getReactionEvents)
        {
            if (await ShouldActivate(effectSource, effectOwner, getReactionEvents.Event))
            {
                getReactionEvents.AddValue(await CreateEvent(effectSource, effectOwner, getReactionEvents.Event), Name);
            }
        }
    }
}
