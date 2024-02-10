namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Characters;

internal class CalculateHealAmount : DndScoreCommand
{
    public CalculateHealAmount(Character character, int amount) : base(character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    public override void InitializeResult()
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
}