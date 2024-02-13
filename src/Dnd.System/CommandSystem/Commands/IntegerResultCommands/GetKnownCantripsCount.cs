namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class GetKnownCantripsCount : DndScoreCommand
{
    public GetKnownCantripsCount(IGameActor character) : base(character)
    {
    }

    protected override void FinalizeResult()
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Defauult", 0);
    }
}
