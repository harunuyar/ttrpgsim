namespace Dnd._5eSRD.Models.Race;

using Dnd._5eSRD.Models.Common;

public class RaceAbilityBonusModel
{
    public APIReference? AbilityScore { get; set; }
    public int? Bonus { get; set; }
}

public class RaceModel : APIReference
{
    public Choice? AbilityBonusOptions { get; set; }
    public List<RaceAbilityBonusModel>? AbilityBonuses { get; set; }
    public string? Age { get; set; }
    public string? Alignment { get; set; }
    public string? LanguageDesc { get; set; }
    public Choice? LanguageOptions { get; set; }
    public List<APIReference>? Languages { get; set; }
    public ECreatureSize? Size { get; set; }
    public string? SizeDescription { get; set; }
    public int? Speed { get; set; }
    public List<APIReference>? StartingProficiencies { get; set; }
    public Choice? StartingProficiencyOptions { get; set; }
    public List<APIReference>? Subraces { get; set; }
    public List<APIReference>? Traits { get; set; }
}
