namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public abstract class DndScoreCommand : DndCommand
{
    public DndScoreCommand(ICharacter character) : base(character)
    {
        Result = IntegerResultWithBonus.Empty(this);
        ShouldCollectBonuses = true;
    }

    public IntegerResultWithBonus Result { get; }

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
