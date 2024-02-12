namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects;

public class AddEffect : DndEventCommand
{
    public AddEffect(IEventListener eventListener, ICharacter character, IEffect effect) : base(eventListener, character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    public override void FinalizeEvent()
    {
        Effect.StartEffect();
        EventResult.SetMessage($"Applied {Effect.Name} to {Effect.Target.Name}");
    }
}
