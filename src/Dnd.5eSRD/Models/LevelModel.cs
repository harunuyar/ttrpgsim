namespace Dnd._5eSRD.Models.Level;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ClassSpecificCreatingSpellSlot
{
    public int? SorceryPointCost { get; set; }
    public int? SpellSlotLevel { get; set; }
}

public class ClassSpecificMartialArt
{
    public int? DiceCount { get; set; }
    public int? DiceValue { get; set; }
}

public class ClassSpecificSneakAttack
{
    public int? DiceCount { get; set; }
    public int? DiceValue { get; set; }
}

public class ClassSpecific
{
    public int? ActionSurges { get; set; }
    public int? ArcaneRecoveryLevels { get; set; }
    public int? AuraRange { get; set; }
    public int? BardicInspirationDie { get; set; }
    public int? BrutalCriticalDice { get; set; }
    public int? ChannelDivinityCharges { get; set; }
    public List<ClassSpecificCreatingSpellSlot>? CreatingSpellSlots { get; set; }
    public double? DestroyUndeadCr { get; set; }
    public int? ExtraAttacks { get; set; }
    public int? FavoredEnemies { get; set; }
    public int? FavoredTerrain { get; set; }
    public int? IndomitableUses { get; set; }
    public int? InvocationsKnown { get; set; }
    public int? KiPoints { get; set; }
    public int? MagicalSecretsMax5 { get; set; }
    public int? MagicalSecretsMax7 { get; set; }
    public int? MagicalSecretsMax9 { get; set; }
    public ClassSpecificMartialArt? MartialArts { get; set; }
    public int? MetamagicKnown { get; set; }
    public int? MysticArcanumLevel6 { get; set; }
    public int? MysticArcanumLevel7 { get; set; }
    public int? MysticArcanumLevel8 { get; set; }
    public int? MysticArcanumLevel9 { get; set; }
    public int? RageCount { get; set; }
    public int? RageDamageBonus { get; set; }
    public ClassSpecificSneakAttack? SneakAttack { get; set; }
    public int? SongOfRestDie { get; set; }
    public int? SorceryPoints { get; set; }
    public int? UnarmoredMovement { get; set; }
    public bool? WildShapeFly { get; set; }
    public double? WildShapeMaxCr { get; set; }
    public bool? WildShapeSwim { get; set; }
}

public class Spellcasting
{
    public int? CantripsKnown { get; set; }
    public int SpellSlotsLevel1 { get; set; }
    public int SpellSlotsLevel2 { get; set; }
    public int SpellSlotsLevel3 { get; set; }
    public int SpellSlotsLevel4 { get; set; }
    public int SpellSlotsLevel5 { get; set; }
    public int? SpellSlotsLevel6 { get; set; }
    public int? SpellSlotsLevel7 { get; set; }
    public int? SpellSlotsLevel8 { get; set; }
    public int? SpellSlotsLevel9 { get; set; }
    public int? SpellsKnown { get; set; }
}

public class SubclassSpecific
{
    public int? AdditionalMagicalSecretsMaxLvl { get; set; }
    public int? AuraRange { get; set; }
}

public class LevelModel : APIReference
{
    public int? AbilityScoreBonuses { get; set; }
    public APIReference? Class { get; set; }
    public ClassSpecific? ClassSpecific { get; set; }
    public List<APIReference>? Features { get; set; }
    [JsonPropertyName("level")]
    public int? LevelNumber { get; set; }
    public int? ProfBonus { get; set; }
    public Spellcasting? Spellcasting { get; set; }
    public APIReference? Subclass { get; set; }
    public SubclassSpecific? SubclassSpecific { get; set; }
}