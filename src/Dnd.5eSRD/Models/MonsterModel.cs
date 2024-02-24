namespace Dnd._5eSRD.Models.Monster;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ActionOption
{
    public string? ActionName { get; set; }
    public int? Count { get; set; }
    public string? Type { get; set; }
}

public class ActionUsage
{
    public string? Type { get; set; }
    public string? Dice { get; set; }
    public int? MinValue { get; set; }
}

public class Action
{
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public int? AttackBonus { get; set; }
    public List<Damage>? Damage { get; set; }
    public DifficultyClass? Dc { get; set; }
    public Choice? Options { get; set; }
    public ActionUsage? Usage { get; set; }
    public string? MultiattackType { get; set; }
    public List<ActionOption>? Actions { get; set; }
    public Choice? ActionOptions { get; set; }
}

public class ArmorClass
{
    public string? Type { get; set; }
    public int? Value { get; set; }
    public List<APIReference>? Armor { get; set; }
    public string? Desc { get; set; }
}

public class ActionDamage
{
    public APIReference? DamageType { get; set; }
    public Dictionary<string, string>? DamageAtCharacterLevel { get; set; }
}

public class LegendaryAction
{
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public int? AttackBonus { get; set; }
    public List<ActionDamage>? Damage { get; set; }
    public DifficultyClass? Dc { get; set; }
}

public class Proficiency
{
    [JsonPropertyName("proficiency")]
    public APIReference? ProficiencyReference { get; set; }
    public int? Value { get; set; }
}

public class Reaction
{
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public DifficultyClass? Dc { get; set; }
}

public class Sense
{
    public string? Blindsight { get; set; }
    public string? Darkvision { get; set; }
    public int? PassivePerception { get; set; }
    public string? Tremorsense { get; set; }
    public string? Truesight { get; set; }
}

public class SpecialAbilityUsage
{
    public string? Type { get; set; }
    public int? Times { get; set; }
    public List<string>? RestTypes { get; set; }
}

public class SpecialAbilitySpell
{
    public string? Name { get; set; }
    public int? Level { get; set; }
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public SpecialAbilityUsage? Usage { get; set; }
}

public class SpecialAbilitySpellcasting
{
    public int? Level { get; set; }
    public APIReference? Ability { get; set; }
    public int? Dc { get; set; }
    public int? Modifier { get; set; }
    public List<string>? ComponentsRequired { get; set; }
    public string? School { get; set; }
    public Dictionary<string, int>? Slots { get; set; }
    public List<SpecialAbilitySpell>? Spells { get; set; }
}

public class SpecialAbility
{
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public int? AttackBonus { get; set; }
    public List<ActionDamage>? Damage { get; set; }
    public DifficultyClass? Dc { get; set; }
    public SpecialAbilitySpellcasting? Spellcasting { get; set; }
    public SpecialAbilityUsage? Usage { get; set; }
}

public class Speed
{
    public string? Burrow { get; set; }
    public string? Climb { get; set; }
    public string? Fly { get; set; }
    public bool? Hover { get; set; }
    public string? Swim { get; set; }
    public string? Walk { get; set; }
}

public class MonsterModel : APIReference
{
    public List<Action>? Actions { get; set; }
    public string? Alignment { get; set; }
    public List<ArmorClass>? ArmorClass { get; set; }
    public double? ChallengeRating { get; set; }
    public int? Charisma { get; set; }
    public List<APIReference>? ConditionImmunities { get; set; }
    public int? Constitution { get; set; }
    public List<string>? DamageImmunities { get; set; }
    public List<string>? DamageResistances { get; set; }
    public List<string>? DamageVulnerabilities { get; set; }
    public int? Dexterity { get; set; }
    public List<APIReference>? Forms { get; set; }
    public string? HitDice { get; set; }
    public int? HitPoints { get; set; }
    public string? HitPointsRoll { get; set; }
    public string? Image { get; set; }
    public int? Intelligence { get; set; }
    public string? Languages { get; set; }
    public List<LegendaryAction>? LegendaryActions { get; set; }
    public List<Proficiency>? Proficiencies { get; set; }
    public List<Reaction>? Reactions { get; set; }
    public Sense? Senses { get; set; }
    public CreatureSize? Size { get; set; }
    public List<SpecialAbility>? SpecialAbilities { get; set; }
    public Speed? Speed { get; set; }
    public int? Strength { get; set; }
    public string? Subtype { get; set; }
    public CreatureType? Type { get; set; }
    public int? Wisdom { get; set; }
    public int? Xp { get; set; }
}