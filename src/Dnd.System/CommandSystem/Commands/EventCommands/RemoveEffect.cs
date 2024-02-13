namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects;

internal class RemoveEffect : DndEventCommand
{
    public RemoveEffect(IEventListener eventListener, IGameActor character, IEffect effect) : base(eventListener, character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        Effect.RemoveEffect();
        EventResult.SetMessage($"Removed {Effect.Name} from {Effect.Target.Name}");
    }
}
