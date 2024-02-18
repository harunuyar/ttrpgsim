namespace Dnd.System.CommandSystem.Commands.BaseCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;

public abstract class DndListCommand<T> : ADndCommand<ListResult<T>>
{
    public DndListCommand(IGameActor character) : base(character)
    {
        Result = new ListResult<T>();
    }

    public override ListResult<T> Result { get; }

    public void AddItem(IDndEntity source, T item)
    {
        if (!IsForceCompleted)
        {
            Result.Add(source, item);
        }
    }

    public void AddItems(IDndEntity source, IEnumerable<T> items)
    {
        if (!IsForceCompleted)
        {
            Result.Add(source, items);
        }
    }

    public void SetItemsAndReturn(IDndEntity source, IEnumerable<T> list)
    {
        if (!IsForceCompleted)
        {
            Result.Set(new List<(IDndEntity, IEnumerable<T>)>() { (source, list) });
        }
    }

    public void AddByOverriding<K>(K newSource, IEnumerable<T> values, Func<K, K, bool> overrides) where K : IDndEntity
    {
        if (!IsForceCompleted)
        {
            Result.AddByOverriding(newSource, values, overrides);
        }
    }
}
