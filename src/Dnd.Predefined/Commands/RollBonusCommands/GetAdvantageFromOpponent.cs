namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantageFromOpponent : ListCommand<EAdvantage>
{
    internal GetAdvantageFromOpponent(IGameActor actor, IRollAction action, IGameActor opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public IRollAction Action { get; }

    public IGameActor Opponent { get; }
}
