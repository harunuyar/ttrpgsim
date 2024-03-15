namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class IsActionAvailable : ValueCommand<bool>
{
    public IsActionAvailable(IGameActor actor, IEventAction action, IGameActor? opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public IEventAction Action { get; }

    public IGameActor? Opponent { get; }

    protected override async Task InitializeResult()
    {
        var canTakeAnyAction = await new CanTakeAnyAction(Actor).Execute();
        if (!canTakeAnyAction.IsSuccess)
        {
            SetError("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Set(canTakeAnyAction);
            ForceComplete();
            return;
        }

        bool fromAction = await Action.IsActionAvailable(Actor);
        SetValue(fromAction, "Default");

        if (fromAction && Opponent is not null)
        {
            var fromOpponent = await new IsActionAvailableFromOpponent(Opponent, Action, Actor).Execute();

            if (!fromOpponent.IsSuccess)
            {
                SetError("IsActionAvailableFromOpponent: " + fromOpponent.ErrorMessage);
                return;
            }

            if (!fromOpponent.Value)
            {
                Set(fromOpponent);
            }
        }
    }
}
