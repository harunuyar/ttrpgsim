namespace Dnd._5eSRD.Client;

using Dnd._5eSRD.Models.Common;

public interface IDndClient
{
    Task<T?> GetObject<T>(string objectIndex);
    Task<List<APIReference>> GetAllReferenceObjects(string objectType);
    Task<List<T>> GetAllObjects<T>(string objectType);
    Task<Dictionary<string, string>> GetObjectTypes();
    Task<List<T>> GetAllObjectsLevelsExclusive<T>(string objectType);
}
