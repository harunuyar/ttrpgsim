namespace Dnd._5eSRD.Client;

using Dnd._5eSRD.Models.Common;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class DndClient : IDndClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DndClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/")
        };

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
        };
    }

    public async Task<T?> GetObject<T>(string objectIndex)
    {
        var response = await _httpClient.GetAsync($"{objectIndex}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch object with index {objectIndex}. Status code: {response.StatusCode}");
        }

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonString, _jsonSerializerOptions);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public async Task<List<APIReference>> GetAllReferenceObjects(string objectType)
    {
        var response = await _httpClient.GetAsync($"{objectType}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch {objectType}. Status code: {response.StatusCode}");
        }

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ApiResponse<APIReference>>(jsonString, _jsonSerializerOptions);

            return responseObject?.Results ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<List<T>> GetAllObjects<T>(string objectType)
    {
        var response = await _httpClient.GetAsync($"{objectType}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch {objectType}. Status code: {response.StatusCode}");
        }

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ApiResponse<APIReference>>(jsonString, _jsonSerializerOptions);

            return (await Task.WhenAll(responseObject?.Results?.Select(async x => await GetObject<T>(objectType + "/" + x.Index!)) ?? []))
                .Where(x => x != null)
                .Select(x => x!)
                .ToList();
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<Dictionary<string, string>> GetObjectTypes()
    {
        var response = await _httpClient.GetAsync("api");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch object types. Status code: {response.StatusCode}");
        }

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString, _jsonSerializerOptions);
            return responseObject ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<List<T>> GetAllObjectsLevelsExclusive<T>(string objectType)
    {
        var response = await _httpClient.GetAsync($"{objectType}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch {objectType}. Status code: {response.StatusCode}");
        }

        try
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<T[]>(jsonString, _jsonSerializerOptions);
            return responseObject?.ToList() ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    private class ApiResponse<T>
    {
        public int? Count { get; set; }
        public List<T>? Results { get; set; }
    }
}