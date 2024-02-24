namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

internal class CanBeTargeted : ValueCommand<bool>
{
    public CanBeTargeted(IGameActor character, IGameActor attacker) : base(character)
    {
        Attacker = attacker;
    }

    public IGameActor Attacker { get; }

    protected override Task InitializeResult()
    {
        SetValue(true, $"{Actor.Name} can be targeted by {Attacker.Name}.");

        return Task.CompletedTask;
    }
}
