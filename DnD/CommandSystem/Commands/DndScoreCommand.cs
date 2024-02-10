namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;

internal abstract class DndScoreCommand : DndCommand
{
    public DndScoreCommand(Character character) : base(character)
    {
        Result = IntegerResultWithBonus.Empty(this);
    }

    public IntegerResultWithBonus Result { get; }

    public override IntegerResultWithBonus Execute()
    {
        InitializeResult();

        if (Result.IsSuccess)
        {
            CollectBonuses();
        }

        return Result;
    }

    public abstract void InitializeResult();
}
