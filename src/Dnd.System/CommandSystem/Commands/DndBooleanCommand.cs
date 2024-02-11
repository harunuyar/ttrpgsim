namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public abstract class DndBooleanCommand : DndCommand
{
    public DndBooleanCommand(ICharacter character) : base(character)
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
