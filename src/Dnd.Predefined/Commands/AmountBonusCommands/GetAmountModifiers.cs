﻿namespace Dnd.Predefined.Commands.DamageBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAmountModifiers : ListCommand<DicePool>
{
    public GetAmountModifiers(IGameActor actor, IAmountAction amountAction, IGameActor? opponent) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor? Opponent { get; }

    protected override async Task InitializeResult()
    {
        await AmountAction.HandleUsageCommand(this);

        if (Opponent is not null)
        {
            var fromOpponent = await new GetAmountModifiersFromOpponent(Opponent, AmountAction, Actor).Execute();

            if (!fromOpponent.IsSuccess)
            {
                SetError("GetAmountModifiersFromOpponent: " + fromOpponent.ErrorMessage);
                return;
            }

            Add(fromOpponent);
        }
    }

    public void AddValue(int value, string message)
    {
        AddValue(DicePool.OfConstant(value), message);
    }
}
