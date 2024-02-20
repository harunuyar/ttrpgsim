namespace Dnd._5eSRD;

using Dnd._5eSRD.Client;
using Dnd._5eSRD.Models.Common;

public class DndContext
{
    public static readonly DndContext Instance = new();

    private readonly DndClient dndClient;
    private readonly Dictionary<string, APIReference> Cache;

    private DndContext()
    {
        dndClient = new DndClient();
        Cache = [];
    }

    public async Task<T?> GetObject<T>(string index) where T : APIReference
    {
        if (Cache.TryGetValue(index, out var cachedObject))
        {
            return cachedObject as T;
        }

        var obj = await dndClient.GetObject<T>(index);
        if (obj != null)
        {
            Cache.Add(index, obj);
        }

        return obj;
    }
}
