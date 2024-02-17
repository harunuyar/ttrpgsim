namespace Dnd.System.CommandSystem.Results;

using Dnd.System.Entities;
using Dnd.System.Entities.DiceModifiers;

public class IntegerResultWithBonus : ICommandResult
{
    public static IntegerResultWithBonus Empty() => new IntegerResultWithBonus(true, null, null, 0, new BonusCollection());

    public static IntegerResultWithBonus Success(IDndEntity source, int baseValue, BonusCollection bonuses) => new IntegerResultWithBonus(true, null, source, baseValue, bonuses);

    public static IntegerResultWithBonus Success(string source, int baseValue) => new IntegerResultWithBonus(true, null, new CustomDndEntity(source), baseValue, new BonusCollection());

    public static IntegerResultWithBonus Failure(string errorMessage) => new IntegerResultWithBonus(false, errorMessage, null, 0, new BonusCollection());

    private IntegerResultWithBonus(bool success, string? errorMessage, IDndEntity? source, int baseValue, BonusCollection bonusCollection)
    {
        IsSuccess = success;
        ErrorMessage = errorMessage;
        BaseSource = source;
        BaseValue = baseValue;
        BonusCollection = bonusCollection;
    }

    public BonusCollection BonusCollection { get; }

    public EAdvantage Advantage => BonusCollection.Advantage;

    public ERollResult RollResult => BonusCollection.RollSuccess;

    public EHitResult HitResult => BonusCollection.HitResult;

    public int Value => BaseValue + BonusCollection.TotalValue;

    public int BaseValue { get; private set; }

    public IDndEntity? BaseSource { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public void SetBaseValue(IDndEntity source, int value)
    {
        BaseSource = source;
        BaseValue = value;
    }

    public void SetBaseValue(string source, int value)
    {
        SetBaseValue(new CustomDndEntity(source), value);
    }

    public void Set(IntegerResultWithBonus other)
    {
        if (other.IsSuccess)
        {
            IsSuccess = true;
            BaseSource = other.BaseSource;
            BaseValue = other.BaseValue;

            foreach (var (bonusSource, bonusValue) in other.BonusCollection.Values)
            {
                BonusCollection.AddBonus(bonusSource, bonusValue);
            }

            foreach (var (advantageSource, advantage) in other.BonusCollection.Advantages)
            {
                BonusCollection.AddAdvantage(advantageSource, advantage);
            }

            foreach (var (rollSuccessSource, rollSuccess) in other.BonusCollection.RollSuccesses)
            {
                BonusCollection.AddRollSuccess(rollSuccessSource, rollSuccess);
            }
        }
        else
        {
            IsSuccess = false;
            ErrorMessage = other.ErrorMessage;
        }
    }

    public void AddAsBonus(string source, IntegerResultWithBonus other)
    {
        AddAsBonus(new CustomDndEntity(source), other);
    }

    public void AddAsBonus(IDndEntity source, IntegerResultWithBonus other)
    {
        BonusCollection.AddBonus(other.BaseSource ?? source, other.BaseValue);

        foreach (var (bonusSource, bonusValue) in other.BonusCollection.Values)
        {
            BonusCollection.AddBonus(bonusSource, bonusValue);
        }

        foreach (var (advantageSource, advantage) in other.BonusCollection.Advantages)
        {
            BonusCollection.AddAdvantage(advantageSource, advantage);
        }

        foreach (var (rollSuccessSource, rollSuccess) in other.BonusCollection.RollSuccesses)
        {
            BonusCollection.AddRollSuccess(rollSuccessSource, rollSuccess);
        }
    }

    public void Reset()
    {
        IsSuccess = true;
        ErrorMessage = null;
        BaseSource = null;
        BaseValue = 0;
        BonusCollection.Reset();
    }

    public override string ToString()
    {
        return IsSuccess ? $"{BaseSource}: {Value}" + Environment.NewLine + BonusCollection.ToString() : ErrorMessage ?? "Unknown error";
    }
}
