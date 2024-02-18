namespace Dnd.System.Entities.Actions;

[Flags]
public enum EUsageLimitation
{
    None = 0,
    ShortRest = 1,
    LongRest = 2,
    PerTurn = 4,
    PerCampaign = 8,
    Charges = 16
}
