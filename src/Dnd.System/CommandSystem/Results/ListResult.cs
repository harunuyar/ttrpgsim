namespace Dnd.System.CommandSystem.Results;

using Dnd.System.Entities;

public class ListResult<T> : ICommandResult
{
    public ListResult()
    {
        IsSuccess = true;
        ValuesPerSource = new List<(IDndEntity Source, IEnumerable<T> Values)>();
    }

    public List<(IDndEntity Source, IEnumerable<T> Values)> ValuesPerSource { get; }

    public IReadOnlyList<T> Values => ValuesPerSource.SelectMany(x => x.Values).ToList();

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void Set(List<(IDndEntity Source, IEnumerable<T> Values)> valuesPerSource)
    {
        IsSuccess = true;
        ValuesPerSource.Clear();
        ValuesPerSource.AddRange(valuesPerSource);
    }

    public void Add(IDndEntity source, IEnumerable<T> values)
    {
        ValuesPerSource.Add((source, values));
    }

    public void Add(IDndEntity source, T value)
    {
        ValuesPerSource.Add((source, new List<T> { value }));
    }

    public void AddByOverriding<K>(K newSource, IEnumerable<T> values, Func<K, K, bool> overrides) where K : IDndEntity
    {
        ValuesPerSource.RemoveAll(x => x.Source is K oldSource && overrides(newSource, oldSource));

        if (!ValuesPerSource.Any(x => x.Source is K oldSource && overrides(oldSource, newSource)))
        {
            Add(newSource, values);
        }
    }

    public void Reset()
    {
        IsSuccess = true;
        ValuesPerSource.Clear();
    }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
