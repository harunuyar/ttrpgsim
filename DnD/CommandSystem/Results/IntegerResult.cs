namespace DnD.CommandSystem.Results;
using TableTopRpg.Commands;

internal class IntegerResult : DndCommandResult
{
    public IntegerResult(ICommand command, int value = 0, bool success = true, string? errorMsg = null) : base(command, success, errorMsg)
    {
        Value = value;
    }

    public virtual int Value { get; }
}
