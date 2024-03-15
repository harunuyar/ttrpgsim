namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CalculateHeal : ScoreCommand
{
    public CalculateHeal(IGameActor character, int amount) : base(character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(Amount, "Base");

        return Task.CompletedTask;
    }
}