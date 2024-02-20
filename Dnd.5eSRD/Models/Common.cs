namespace Dnd._5eSRD.Models.Common;

using System.Collections.Generic;

public class APIReference
{
    public string? Index { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
}

public class Choice
{
    public string? Desc { get; set; }
    public int? Choose { get; set; }
    public string? Type { get; set; }
    public OptionSet? From { get; set; }
}

public class AreaOfEffect
{
    public int? Size { get; set; }
    public AreaOfEffectType? Type { get; set; }
}

public enum AreaOfEffectType
{
    Sphere,
    Cube,
    Cylinder,
    Line,
    Cone
}

public class OptionSet
{
    public OptionSetType? OptionSetType { get; set; }

    // options_array
    public List<Option>? Options { get; set; }

    // equipment_category
    public APIReference? EquipmentCategory { get; set; }

    // resource_list
    public string? ResourceListUrl { get; set; }
}

public enum OptionSetType
{
    OptionsArray,
    EquipmentCategory,
    ResourceList
}

public class DifficultyClass
{
    public APIReference? DcType { get; set; }
    public int? DcValue { get; set; }
    public SuccessType? SuccessType { get; set; }
}

public enum SuccessType
{
    None,
    Half,
    Other
}

public class Damage
{
    public APIReference? DamageType { get; set; }
    public string? DamageDice { get; set; }
}

public class Option
{
    public OptionType? OptionType { get; set; }

    // reference
    public APIReference? Item { get; set; }

    // action
    public string? ActionName { get; set; }
    public int? Count { get; set; }
    public ActionType? Type { get; set; }
    public string? Notes { get; set; }

    // multiple
    public List<Option>? Items { get; set; }

    // choice
    public Choice? Choice { get; set; }

    // string
    public string? String { get; set; }

    // ideal
    public string? Desc { get; set; }
    public List<APIReference>? Alignments { get; set; }

    // countedReference
    // public int? Count { get; set; }
    public APIReference? Of { get; set; }
    public List<Prerequisite>? Prerequisites { get; set; }

    // scorePrerequisite
    public APIReference? AbilityScore { get; set; }
    public int? MinimumScore { get; set; }

    // abilityBonus
    //public APIReference? AbilityScore { get; set; }
    public int? Bonus { get; set; }

    // breath
    public string? Name { get; set; }
    public DifficultyClass? Dc { get; set; }
    public List<Damage>? Damage { get; set; }

    // damage
    public APIReference? DamageType { get; set; }
    public string? DamageDice { get; set; }
    //public string? Notes { get; set; }
}

public enum OptionType
{
    Reference,
    Action,
    Multiple,
    Choice,
    String,
    Ideal,
    CountedReference,
    ScorePrerequisite,
    AbilityBonus,
    Breath,
    Damage
}

public enum ActionType
{
    Melee,
    Ranged,
    Ability,
    Magic
}

public class Prerequisite
{
    public string? Type { get; set; }
    public APIReference? Proficiency { get; set; }
}