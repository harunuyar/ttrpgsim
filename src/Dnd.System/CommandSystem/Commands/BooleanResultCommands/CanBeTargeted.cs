namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

internal class CanBeTargeted : DndBooleanCommand
{
    public CanBeTargeted(IGameActor character, IGameActor attacker) : base(character)
    {
        Attacker = attacker;
    }

    public IGameActor Attacker { get; }

    protected override void InitializeResult()
    {
        SetValue(true, $"{Actor.Name} can be targeted by {Attacker.Name}.");
    }
}
