namespace Dnd._5eSRD.Models.Background;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Background : APIReference
{
    public List<APIReference>? StartingProficiencies { get; set; }
    public Choice? LanguageOptions { get; set; }
    public List<Equipment>? StartingEquipment { get; set; }
    public List<Choice>? StartingEquipmentOptions { get; set; }
    public Feature? Feature { get; set; }
    public Choice? PersonalityTraits { get; set; }
    public Choice? Ideals { get; set; }
    public Choice? Bonds { get; set; }
    public Choice? Flaws { get; set; }
}

public class Equipment
{
    [JsonPropertyName("equipment")]
    public APIReference? EquipmentReference { get; set; }
    public int? Quantity { get; set; }
}

public class Feature
{
    public string? Name { get; set; }
    public List<string>? Desc { get; set; }
}