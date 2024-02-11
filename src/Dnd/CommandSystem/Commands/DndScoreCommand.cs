namespace Dnd.CommandSystem.Commands;

using Dnd.CommandSystem.Results;
using Dnd.Entities.Characters;

public abstract class DndScoreCommand : DndCommand
{
    public DndScoreCommand(Character character) : base(character)
    {
        Result = IntegerResultWithBonus.Empty(this);
        ShouldCollectBonuses = true;
    }

    internal IntegerResultWithBonus Result { get; }

    protected bool ShouldCollectBonuses { get; set; }

    public override IntegerResultWithBonus Execute()
    {
        Result.Reset();

        InitializeResult();

        if (ShouldCollectBonuses && Result.IsSuccess)
        {
            CollectBonuses();
        }

        return Result;
    }

    public abstract void InitializeResult();
}
