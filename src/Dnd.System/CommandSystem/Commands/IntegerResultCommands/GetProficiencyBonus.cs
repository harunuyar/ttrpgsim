namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class GetProficiencyBonus : DndScoreCommand
{
    public GetProficiencyBonus(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
