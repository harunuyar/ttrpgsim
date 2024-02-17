namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public abstract class GetAttackModifier : DndScoreCommand
{
    public GetAttackModifier(IGameActor character, IGameActor? target) : base(character)
    {
        Target = target;
    }

    public IGameActor? Target { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
