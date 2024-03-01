namespace Dnd.System.Entities.Action;

[Flags]
public enum EReactionType : uint
{
    None            = 0x0,
    // When
    Before          = 1u << 0,
    After           = 1u << 1,
    // By who
    Self            = 1u << 2,
    Enemy           = 1u << 3,
    Ally            = 1u << 4,
    // To whom
    ToSelf          = 1u << 5,
    ToEnemy         = 1u << 6,
    ToAlly          = 1u << 7,
    // What
    SpellCast       = 1u << 8,
    AttackRoll      = 1u << 9,
    DamageRoll      = 1u << 10,
    HealRoll        = 1u << 11,
    SavingThrow     = 1u << 12,
    AbilityCheck    = 1u << 13,
    SkillCheck      = 1u << 14,
    Walk            = 1u << 15,
    // Where          
    Nearby          = 1u << 16,
    FarAway         = 1u << 17,
}
