namespace Dnd._5eSRD.JsonDeserializer;

using System.Text.Json.Serialization;

public class SnakeCaseLowerJsonStringEnumConverter : JsonStringEnumConverter
{
    public SnakeCaseLowerJsonStringEnumConverter() : base(System.Text.Json.JsonNamingPolicy.SnakeCaseLower, true)
    {
    }
}
