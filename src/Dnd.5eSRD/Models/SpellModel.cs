namespace Dnd._5eSRD.Models.Spell;

using Dnd._5eSRD.Models.Common;

public class Damage
{
    public Dictionary<int, string>? DamageAtSlotLevel { get; set; }
    public Dictionary<int, string>? DamageAtCharacterLevel { get; set; }
    public APIReference? DamageType { get; set; }
}

public enum EDcSuccess
{
    None,
    Half,
    Full,
    Other
}

public class DC
{
    public EDcSuccess? DcSuccess { get; set; }
    public APIReference? DcType { get; set; }
    public string? Desc { get; set; }
}

public enum ESpellComponent
{
    V,
    S,
    M
}

public enum EAttackType
{
    None,
    Melee,
    Ranged
}

public class SpellModel : APIReference
{
    public AreaOfEffectModel? AreaOfEffect { get; set; }
    public EAttackType? AttackType { get; set; }
    public string? CastingTime { get; set; }
    public List<APIReference>? Classes { get; set; }
    public List<ESpellComponent>? Components { get; set; }
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
