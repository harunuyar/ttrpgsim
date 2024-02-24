namespace Dnd._5eSRD.Models.Class;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Equipment
{
    [JsonPropertyName("equipment")]
    public APIReference? EquipmentReference { get; set; }
    public int? Quantity { get; set; }
}

public class StartingEquipmentOption
{
    public APIReference? Equipment { get; set; }
    public int? Quantity { get; set; }
}

public class SpellcastingInfo
{
    public List<string>? Desc { get; set; }
    public string? Name { get; set; }
}

public class Spellcasting
{
    public List<SpellcastingInfo>? Info { get; set; }
    public int? Level { get; set; }
    public APIReference? SpellcastingAbility { get; set; }
}

public class MultiClassingPrereq
{
    public APIReference? AbilityScore { get; set; }
    public int? MinimumScore { get; set; }
}

public class MultiClassing
{
    public List<MultiClassingPrereq>? Prerequisites { get; set; }
    public Choice? PrerequisiteOptions { get; set; }
    public List<APIReference>? Proficiencies { get; set; }
    public List<Choice>? ProficiencyChoices { get; set; }
}

public class ClassModel : APIReference
{
    public string? ClassLevels { get; set; }
    public MultiClassing? MultiClassing { get; set; }
    public int? HitDie { get; set; }
    public List<APIReference>? Proficiencies { get; set; }
    public List<Choice>? ProficiencyChoices { get; set; }
    public List<APIReference>? SavingThrows { get; set; }
    public Spellcasting? Spellcasting { get; set; }
    public string? Spells { get; set; }
    public List<Equipment>? StartingEquipment { get; set; }
    public List<StartingEquipmentOption>? StartingEquipmentOptions { get; set; }
    public List<APIReference>? Subclasses { get; set; }
}