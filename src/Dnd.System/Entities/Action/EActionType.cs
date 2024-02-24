namespace Dnd.System.Entities.Action;

[Flags]
public enum EActionType : byte
{
    None = 0,
    MainAction = 1,
    BonusAction = 2,
    Reaction = 4,
    FreeAction = 8
}
