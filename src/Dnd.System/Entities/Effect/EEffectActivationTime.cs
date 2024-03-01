namespace Dnd.System.Entities.Effect;

[Flags]
public enum EEffectActivationTime : uint
{
    None                    = 0x0,
    // Timing
    TargetTurnStart         = 1u << 0,
    TargetTurnEnd           = 1u << 1,
    SourceTurnStart         = 1u << 2,
    SourceTurnEnd           = 1u << 3,
    StartOfRound            = 1u << 4,
    EndOfRound              = 1u << 5,
    // Trigger
    OnHit                   = 1u << 6,
    OnMiss                  = 1u << 7,
    WhenDamaged             = 1u << 8,
    WhenHealed              = 1u << 9,
    // Conditions
    UnderCondition          = 1u << 10,
    // Environmental
    InDarkness              = 1u << 11,
    InLight                 = 1u << 12,
    // Specific Actions or Events
    OnSpellCast             = 1u << 13,
    OnAbilityUse            = 1u << 14,
    OnMovement              = 1u << 15,
    OnShortRest             = 1u << 16,
    OnLongRest              = 1u << 17,
    Instant                 = 1u << 18,
    // Movement relative to areas
    EntersArea              = 1u << 19,
    ExitsArea               = 1u << 20,
    MovesWithinArea         = 1u << 21,
    // Clarifying perspective
    StartOfCasterTurn       = 1u << 22,
    EndOfCasterTurn         = 1u << 23,
    StartOfTargetTurn       = 1u << 24,
    EndOfTargetTurn         = 1u << 25,
}
