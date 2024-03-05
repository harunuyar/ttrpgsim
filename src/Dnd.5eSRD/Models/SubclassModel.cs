namespace Dnd._5eSRD.Models.Subclass;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Common;
using System.Text.Json.Serialization;

public class SpellPrerequisite
{
    public string? Index { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Url { get; set; }
}

public class Spell
{
    public List<SpellPrerequisite>? Prerequisites { get; set; }
    [JsonPropertyName("spell")]
    public APIReference? SpellReference { get; set; }
}

public class SubclassModel : APIReference
{
    public APIReference? Class { get; set; }
    public List<string>? Desc { get; set; }
    public List<Spell>? Spells { get; set; }
    public string? SubclassFlavor { get; set; }
    public string? SubclassLevels { get; set; }

    // This is not in the original model
    public Spellcasting? Spellcasting { get; set; }
}