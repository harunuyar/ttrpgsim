namespace Dnd._5eSRD.Models.Subrace;

using Dnd._5eSRD.Models.Common;

public class AbilityBonus
{
    public APIReference? AbilityScore { get; set; }
    public int? Bonus { get; set; }
}

public class SubraceModel : APIReference
{
    public List<AbilityBonus>? AbilityBonuses { get; set; }
    public string? Desc { get; set; }
    public Choice? LanguageOptions { get; set; }
    public APIReference? Race { get; set; }
    public List<APIReference>? RacialTraits { get; set; }
    public List<APIReference>? StartingProficiencies { get; set; }
}