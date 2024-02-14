namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public abstract class DndListCommand<T> : ADndCommand<ListResult<T>>
{
    public DndListCommand(IGameActor character) : base(character)
    {
        Result = new ListResult<T>();
    }

    public override ListResult<T> Result { get; }

    public void AddItem(T item)
    {
        if (!IsForceCompleted)
        {
            Result.Values.Add(item);
        }
    }

    public void AddItems(IEnumerable<T> items)
    {
        if (!IsForceCompleted)
        {
            Result.Values.AddRange(items);
        }
    }

    public void SetItemsAndReturn(List<T> list)
    {
        Result.Set(list);
    }
}
