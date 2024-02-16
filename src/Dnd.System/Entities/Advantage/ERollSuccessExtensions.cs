namespace Dnd.System.Entities.Advantage;

public static class ERollSuccessExtensions
{
    public static bool IsCriticalFailure(this ERollResult rollSuccess)
    {
        return rollSuccess == ERollResult.CriticalFailure;
    }

    public static bool IsFailure(this ERollResult rollSuccess)
    {
        return (rollSuccess & (ERollResult.Failure | ERollResult.CriticalFailure)) != 0
            && (rollSuccess & ~(ERollResult.Failure | ERollResult.CriticalFailure)) == 0;
    }

    public static bool IsSuccess(this ERollResult rollSuccess)
    {
        return (rollSuccess & (ERollResult.Success | ERollResult.CriticalSuccess)) != 0
            && (rollSuccess & ~(ERollResult.Success | ERollResult.CriticalSuccess)) == 0;
    }

    public static bool IsCriticalSuccess(this ERollResult rollSuccess)
    {
        return rollSuccess == ERollResult.CriticalSuccess;
    }

    public static bool IsEmpty(this ERollResult rollSuccess)
    {
        return rollSuccess == 0;
    }
}
