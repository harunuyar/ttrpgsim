namespace Dnd.CommandSystem.Commands;

using Dnd.CommandSystem.Results;
using Dnd.Entities;
using Dnd.Entities.Characters;

public abstract class DndBooleanCommand : DndCommand
{
    public DndBooleanCommand(Character character, bool defaultValue = false) : base(character)
    {
        Result = BooleanResult.Empty(this);
        ShouldCollectBonuses = true;
    }

    public BooleanResult Result { get; }

    protected bool ShouldCollectBonuses { get; set; }

    public override BooleanResult Execute()
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
