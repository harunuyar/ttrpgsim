namespace Dnd.System.CommandSystem.Commands.ValueCommands;

using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;

public class GetRollSuccess : DndValueCommand<ERollResult>
{
    public GetRollSuccess(IGameActor character, int targetValue, int rollResult, int modifiers) : base(character)
    {
        TargetValue = targetValue;
        RollResult = rollResult;
        Modifiers = modifiers;
    }

    public int TargetValue { get; }

    public int RollResult { get; }

    public int Modifiers { get; }

    protected override void InitializeResult()
    {
        if (RollResult == 20)
        {
            Result.Value = ERollResult.CriticalSuccess;
        }
        else if (RollResult == 1)
        {
            Result.Value = ERollResult.CriticalFailure;
        }
        else if (RollResult + Modifiers >= TargetValue)
        {
            Result.Value = ERollResult.Success;
        }
        else
        {
            Result.Value = ERollResult.Failure;
        }
    }
}
