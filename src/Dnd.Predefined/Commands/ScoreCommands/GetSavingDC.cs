namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class GetSavingDC : ScoreCommand
{
    public GetSavingDC(IGameActor character, ISavingThrowAction savingThrowAction) : base(character)
    {
        SavingThrowAction = savingThrowAction;
    }

    public ISavingThrowAction SavingThrowAction { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(8, "Base");

        return Task.CompletedTask;
    }
}
