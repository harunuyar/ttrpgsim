namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class GetKnownSpellsCount : DndScoreCommand
{
    public GetKnownSpellsCount(IGameActor character) : base(character)
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
