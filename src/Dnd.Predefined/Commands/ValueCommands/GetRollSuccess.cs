namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetRollSuccess : ValueCommand<ERollResult>
{
    public GetRollSuccess(IGameActor character, IGameActor? target, int targetValue, int rollResult, int modifiers) : base(character)
    {
        Target = target;
        TargetValue = targetValue;
        RollResult = rollResult;
        Modifiers = modifiers;
    }

    public IGameActor? Target { get; }

    public int TargetValue { get; }

    public int RollResult { get; }

    public int Modifiers { get; }

    protected override async Task InitializeResult()
    {
        if (RollResult == 20)
        {
            SetValue(ERollResult.CriticalSuccess, "Critical Success!");
        }
        else if (RollResult == 1)
        {
            SetValue(ERollResult.CriticalFailure, "Critical Failure!");
        }
        else if (RollResult + Modifiers >= TargetValue)
        {
            SetValue(ERollResult.Success, "Success!");
        }
        else
        {
            SetValue(ERollResult.Failure, "Failure!");
        }

        if (Target is not null)
        {
            var rollSuccessAgainst = await new GetRollSuccessAgainst(Target, Actor, Result.Value).Execute();

            if (!rollSuccessAgainst.IsSuccess)
            {
                SetError("GetRollSuccessAgainst: " + rollSuccessAgainst.ErrorMessage);
                return;
            }

            if (rollSuccessAgainst.Value != ERollResult.None)
            {
                SetValue(rollSuccessAgainst.Value, rollSuccessAgainst.Message);
                return;
            }
        }
    }
}
