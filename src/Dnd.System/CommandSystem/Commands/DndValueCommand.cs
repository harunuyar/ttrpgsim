namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public class DndValueCommand<T> : ADndCommand<ValueResult<T>>
{
    public DndValueCommand(IGameActor character) : base(character)
    {
        Result = ValueResult<T>.Empty();
    }

    public override ValueResult<T> Result { get; }

    public void SetValue(T value)
    {
        if (!IsForceCompleted)
        {
            Result.Value = value;
        }
    }

    public void SetValueAndReturn(T value)
    {
        if (!IsForceCompleted)
        {
            Result.Value = value;
            ForceComplete();
        }
    }
}
