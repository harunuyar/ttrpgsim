namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities;
using DnD.Entities.Characters;

internal abstract class DndBooleanCommand : DndCommand
{
    public DndBooleanCommand(Character character, bool defaultValue = false) : base(character)
    {
        Result = BooleanResult.Success(this, new CustomDndEntity("Default"), defaultValue);
    }

    public BooleanResult Result { get; }

    public override BooleanResult Execute()
    {
        InitializeResult();
        CollectBonuses();
        return Result;
    }

    public abstract void InitializeResult();
}
