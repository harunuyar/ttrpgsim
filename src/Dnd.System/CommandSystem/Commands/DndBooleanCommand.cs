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

    public void SetValue(IDndEntity? bonusProvider, bool value, string message)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(bonusProvider, value, message);
        }
    }

    public void SetValueAndReturn(IDndEntity? bonusProvider, bool value, string message)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(bonusProvider, value, message);
            ForceComplete();
        }
    }

    public void SetValue(bool value, string message)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(null, value, message);
        }
    }

    public void SetValueAndReturn(bool value, string message)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(null, value, message);
            ForceComplete();
        }
    }
}
