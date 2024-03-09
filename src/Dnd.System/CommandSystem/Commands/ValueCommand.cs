namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActor;

public class ValueCommand<T> : ACommand<ValueResult<T>>
{
    public ValueCommand(IGameActor actor) : base(actor)
    {
        Result = ValueResult<T>.Empty();
    }

    public override ValueResult<T> Result { get; }

    public void SetValue(T value, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.SetValue(message, value);
    }

    public void Set(ValueResult<T> other)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.Set(other);
    }
}
