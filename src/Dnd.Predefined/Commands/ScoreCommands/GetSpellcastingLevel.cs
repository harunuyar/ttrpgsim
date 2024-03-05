namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSpellcastingLevel : ScoreCommand
{
    public GetSpellcastingLevel(IGameActor actor) : base(actor)
    {
    }

    protected override Task InitializeResult()
    {
        SetBaseValue(0, "Base");
        return Task.CompletedTask;
    }
}
