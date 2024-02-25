namespace Dnd._5eSRD.Models.Trait;

using Dnd._5eSRD.Models.Common;

public class Proficiency
{
    public string? Index { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
}

public class ActionDamage
{
    public APIReference? DamageType { get; set; }
    public Dictionary<string, string>? DamageAtCharacterLevel { get; set; }
}

public class Usage
{
    public string? Type { get; set; }
    public int? Times { get; set; }
}

public class Action
{
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public Usage? Usage { get; set; }
    public DifficultyClass? Dc { get; set; }
    public List<ActionDamage>? Damage { get; set; }
    public AreaOfEffectModel? AreaOfEffect { get; set; }
}

public class TraitSpecific
{
    public Choice? SubtraitOptions { get; set; }
    public Choice? SpellOptions { get; set; }
    public APIReference? DamageType { get; set; }
    public Action? BreathWeapon { get; set; }
}

public class TraitModel : APIReference
{
    public List<string>? Desc { get; set; }
    public List<APIReference>? Proficiencies { get; set; }
    public Choice? ProficiencyChoices { get; set; }
    public Choice? LanguageOptions { get; set; }
    public List<APIReference>? Races { get; set; }
    public List<APIReference>? Subraces { get; set; }
    public APIReference? Parent { get; set; }
    public TraitSpecific? TraitSpecific { get; set; }
}
