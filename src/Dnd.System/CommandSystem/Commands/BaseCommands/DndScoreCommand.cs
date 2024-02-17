namespace Dnd.System.CommandSystem.Commands.BaseCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;

public abstract class DndScoreCommand : ADndCommand<IntegerResultWithBonus>
{
    public DndScoreCommand(IGameActor character) : base(character)
    {
        Result = IntegerResultWithBonus.Empty();
    }

    public override IntegerResultWithBonus Result { get; }

    public void AddBonus(IDndEntity bonusProvider, int value)
    {
        if (!IsForceCompleted)
        {
            Result.BonusCollection.AddBonus(bonusProvider, value);
        }
    }

    public void AddAdvantage(IDndEntity bonusProvider, EAdvantage value)
    {
        if (!IsForceCompleted)
        {
            Result.BonusCollection.AddAdvantage(bonusProvider, value);
        }
    }

    public void SetBaseValue(IDndEntity bonusProvider, int value)
    {
        if (!IsForceCompleted)
        {
            Result.SetBaseValue(bonusProvider, value);
        }
    }

    public void SetAutoSuccess(IDndEntity bonusProvider)
    {
        if (!IsForceCompleted)
        {
            Result.BonusCollection.AddRollSuccess(bonusProvider, ERollResult.CriticalSuccess);
        }
    }

    public void SetAutoFailure(IDndEntity bonusProvider)
    {
        if (!IsForceCompleted)
        {
            Result.BonusCollection.AddRollSuccess(bonusProvider, ERollResult.CriticalFailure);
        }
    }

    public void SetValueAndReturn(IDndEntity bonusProvider, int value)
    {
        if (!IsForceCompleted)
        {
            Result.SetBaseValue(bonusProvider, value);
            Result.BonusCollection.Values.Clear();
            Result.BonusCollection.Advantages.Clear();
            ForceComplete();
        }
    }
}
