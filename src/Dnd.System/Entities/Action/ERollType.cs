namespace Dnd.System.Entities.Action;

public enum ESuccessRollType : int
{
    None        = 0,
    Attack      = 1,
    Save        = 2,
    Skill       = 4,
    DeathSave   = 8,
    Other       = 16,
}

public enum EAmountRollType : int
{
    None        = 0,
    Damage      = 1,
    Healing     = 2,
    Initiative  = 4,
    HitPoints   = 8,
    Other       = 16,
}