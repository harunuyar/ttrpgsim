namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetRollSuccessAgainst : ValueCommand<ERollResult>
{
    internal GetRollSuccessAgainst(IGameActor character, IGameActor attacker, ERollResult normalResult) : base(character)
    {
        Attacker = attacker;
        NormalResult = normalResult;
    }

    public IGameActor Attacker { get; }

    public ERollResult NormalResult { get; }

    protected override Task InitializeResult()
    {
        SetValue(NormalResult, "Default");

        return Task.CompletedTask;
    }
}
