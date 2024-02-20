namespace Dnd._5eSRD.Models.Spell;

using Dnd._5eSRD.Models.Common;

public class Damage
{
    public Dictionary<int, string>? DamageAtSlotLevel { get; set; }
    public Dictionary<int, string>? DamageAtCharacterLevel { get; set; }
    public APIReference? DamageType { get; set; }
}

public class DC
{
    public string? DcSuccess { get; set; }
    public APIReference? DcType { get; set; }
    public string? Desc { get; set; }
}

public class Spell : APIReference
{
    public AreaOfEffect? AreaOfEffect { get; set; }
    public string? AttackType { get; set; }
    public string? CastingTime { get; set; }
    public List<APIReference>? Classes { get; set; }
    public List<string>? Components { get; set; }
    public bool? Concentration { get; set; }
    public Damage? Damage { get; set; }
    public DC? Dc { get; set; }
    public List<string>? Desc { get; set; }
    public string? Duration { get; set; }
    public Dictionary<int, string>? HealAtSlotLevel { get; set; }
    public List<string>? HigherLevel { get; set; }
    public int? Level { get; set; }
    public string? Material { get; set; }
    public string? Range { get; set; }
    public bool? Ritual { get; set; }
    public APIReference? School { get; set; }
    public List<APIReference>? Subclasses { get; set; }
}
