namespace Dnd.Context;

using Dnd._5eSRD.Client;
using Dnd._5eSRD.Models;

public class DndContext
{
    public static readonly DndContext Instance = new(new DndClient(), new CallbackLogger());

    private readonly IDndClient dndClient;
    private readonly Dictionary<string, IAPIReference> cache;

    public ILogger Logger { get; }

    private DndContext(IDndClient dndClient, ILogger logger)
    {
        this.dndClient = dndClient;
        this.Logger = logger;
        cache = [];
    }

    public async Task<T?> GetObject<T>(string index) where T : class, IAPIReference
    {
        if (cache.TryGetValue(index, out var cachedObject))
        {
            return cachedObject as T;
        }

        var obj = await dndClient.GetObject<T>(index);
        if (obj != null)
        {
            cache.Add(index, obj);
        }

        return obj;
    }
}
