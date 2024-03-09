namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

internal class RemoveEffect : VoidCommand
{
    public RemoveEffect(IGameActor character, IEffect effect) : base(character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override Task FinalizeResult()
    {
        Effect.Source.EffectsTable.RemoveCausedEffect(Effect);
        return Task.CompletedTask;
    }
}
