namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class GetMaxKnownCantripsCount : DndScoreCommand
{
    public GetMaxKnownCantripsCount(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Default", 0);
    }
}
