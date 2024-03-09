namespace Dnd.System.Entities.Effect;

[Flags]
public enum EEffectActivationTime : uint
{
    None                    = 0x0,
    // Timing
    TurnStart               = 1u << 0,
    TurnEnd                 = 1u << 1,
    StartOfRound            = 1u << 2,
    EndOfRound              = 1u << 3,
    // Trigger
    OnHit                   = 1u << 4,
    OnMiss                  = 1u << 5,
    WhenDamaged             = 1u << 6,
    WhenHealed              = 1u << 7,
    // Environmental
    InDarkness              = 1u << 8,
    InLight                 = 1u << 9,
    // Specific Actions or Events
    OnSpellCast             = 1u << 10,
    OnAbilityUse            = 1u << 11,
    OnMovement              = 1u << 12,
    OnShortRest             = 1u << 13,
    OnLongRest              = 1u << 14,
    Instant                 = 1u << 15,
    // Movement relative to areas
    EntersArea              = 1u << 16,
    ExitsArea               = 1u << 17,
    MovesWithinArea         = 1u << 18,
    // Who
    Caster                  = 1u << 19,
    Target                  = 1u << 20,
}
