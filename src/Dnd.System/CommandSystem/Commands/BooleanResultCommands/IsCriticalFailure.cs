namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;

public class IsCriticalFailure : DndBooleanCommand
{
    public IsCriticalFailure(IGameActor character, int rollResult) : base(character)
    {
        RollResult = rollResult;
    }

    public int RollResult { get; }

    protected override void InitializeResult()
    {
        if (RollResult == 1)
        {
            Result.SetValue(true, "Critical failure!");
        }
        else
        {
            Result.SetValue(false, "You didn't roll a critical failure.");
        }
    }
}
