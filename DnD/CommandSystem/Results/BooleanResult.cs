namespace DnD.CommandSystem.Results;

using TableTopRpg.Commands;

internal class BooleanResult : DndCommandResult
{
    public BooleanResult(ICommand command, bool value = false, bool success = true, string? errorMsg = null) : base(command, success, errorMsg)
    {
        Value = value;
    }

    public virtual bool Value { get; }
}
