namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class AddEffect : VoidCommand
{
    public AddEffect(IGameActor character, IEffect effect) : base(character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override Task FinalizeResult()
    {
        Effect.Source.EffectsTable.AddCausedEffect(Effect);
        SetMessage($"Applied {Effect.Name} to {Effect.Target.Name}.");
        return Task.CompletedTask;
    }
}
