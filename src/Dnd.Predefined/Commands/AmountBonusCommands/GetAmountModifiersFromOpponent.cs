namespace Dnd.Predefined.Commands.DamageBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetAmountModifiersFromOpponent : ListCommand<int>
{
    public GetAmountModifiersFromOpponent(IGameActor actor, IAmountAction amountAction, IGameActor opponent) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor Opponent { get; }
}
