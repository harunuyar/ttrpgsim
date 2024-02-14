namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;

public abstract class DndBooleanCommand : ADndCommand<BooleanResult>
{
    public DndBooleanCommand(IGameActor character) : base(character)
    {
        Result = BooleanResult.Empty();
        ShouldVisitEntities = true;
    }

    public override BooleanResult Result { get; }

    public void SetValue(IBonusProvider bonusProvider, bool value)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(bonusProvider, value);
        }
    }

    public void SetValueAndReturn(IBonusProvider bonusProvider, bool value)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(bonusProvider, value);
            ForceComplete();
        }
    }
}
