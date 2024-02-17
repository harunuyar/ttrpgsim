namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public abstract class GetAttackModifierAgainst : DndScoreCommand
{
    internal GetAttackModifierAgainst(IGameActor character, IGameActor attacker) : base(character)
    {
        Attacker = attacker;
    }

    public IGameActor Attacker { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
