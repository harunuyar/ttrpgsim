namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;

public class IsCriticalSuccess : DndBooleanCommand
{
    public IsCriticalSuccess(IGameActor character, int rollResult) : base(character)
    {
        RollResult = rollResult;
    }

    public int RollResult { get; }

    protected override void InitializeResult()
    {
        if (RollResult == 20)
        {
            Result.SetValue(true, "Critical hit!");
        }
        else
        {
            Result.SetValue(false, "You didn't roll a critical hit.");
        }
    }
}
