namespace Dnd.System.Entities.Action;

[Flags]
public enum EReactionType : uint
{
    None            = 0x00000000,
    // When
    Before          = 0x00000001,
    After           = 0x00000002,
    // By who
    Self            = 0x00000004,
    Enemy           = 0x00000008,
    Ally            = 0x00000010,
    // To whom
    ToSelf          = 0x00000020,
    ToEnemy         = 0x00000040,
    ToAlly          = 0x00000080,
    // What
    AttackAttempt   = 0x00000100,
    AttackRoll      = 0x00000200,
    DamageRoll      = 0x00000400,
    SavingThrow     = 0x00000800,
    AbilityCheck    = 0x00001000,
    SkillCheck      = 0x00002000,
    Walk            = 0x00004000,
    // Where
    Nearby          = 0x00008000,
    FarAway         = 0x00010000,
}
