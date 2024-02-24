namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActor;

public class ListCommand<T> : ACommand<ListResult<T>>
{
    public ListCommand(IGameActor actor) : base(actor)
    {
        Result = ListResult<T>.Empty();
    }

    protected override ListResult<T> Result { get; }

    public void AddValues(IEnumerable<T> values, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.AddValues(message, values);
    }

    public void AddValue(T value, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.Values.Add((message, value));
    }

    public void Add(ListResult<T> other)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.Add(other);
    }

    public void SetValue(T value, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.SetValues([(message, value)]);
    }

    public void SetValues(List<(string, T)> values)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.SetValues(values);
    }

    public void Set(ListResult<T> other)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.Set(other);
    }
}
