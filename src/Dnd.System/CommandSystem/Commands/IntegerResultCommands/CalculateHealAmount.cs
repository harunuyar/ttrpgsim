namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class CalculateHealAmount : DndScoreCommand
{
    public CalculateHealAmount(IGameActor character, int amount) : base(character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    protected override void InitializeResult()
    {
        var getMaxHP = new GetMaxHP(Character);
        var maxHPResult = getMaxHP.Execute();

        if (maxHPResult.IsSuccess)
        {
            int maxHeal = maxHPResult.Value - Character.HitPoints.CurrentHitPoints;
            Result.SetBaseValue("Base", Math.Min(Amount, maxHeal));
        }
        else
        {
            Result.SetError(maxHPResult.ErrorMessage ?? "Unknown");
        }
    }

    protected override void FinalizeResult()
    {
    }
}