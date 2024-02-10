namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities;
using DnD.Entities.Characters;

internal abstract class DndBooleanCommand : DndCommand
{
    public DndBooleanCommand(Character character, bool defaultValue = false) : base(character)
    {
        Result = BooleanResult.Success(this, new CustomDndEntity("Default"), defaultValue);
        ShouldCollectBonuses = true;
    }

    public BooleanResult Result { get; }

    protected bool ShouldCollectBonuses { get; set; }

    public override BooleanResult Execute()
    {
        InitializeResult();

        if (ShouldCollectBonuses && Result.IsSuccess)
        {
            CollectBonuses();
        }
        
        return Result;
    }

    public abstract void InitializeResult();
}
