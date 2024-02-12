namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects;

internal class RemoveEffect : DndEventCommand
{
    public RemoveEffect(IEventListener eventListener, ICharacter character, IEffect effect) : base(eventListener, character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    public override void FinalizeEvent()
    {
        Effect.RemoveEffect();
        EventResult.SetMessage($"Removed {Effect.Name} from {Effect.Target.Name}");
    }
}
