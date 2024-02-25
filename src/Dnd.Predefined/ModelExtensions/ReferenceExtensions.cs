namespace Dnd.Predefined.ModelExtensions;

using Dnd._5eSRD.Models;
using Dnd._5eSRD.Models.Common;
using Dnd.Context;

public static class ReferenceExtensions
{
    public static Task<T?> GetModel<T>(this IAPIReference reference) where T : APIReference
    {
        if (reference.Url == null)
        {
            return Task.FromResult<T?>(null);
        }

        return DndContext.Instance.GetObject<T>(reference.Url);
    }
}
