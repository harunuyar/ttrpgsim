namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class CanAttackTarget : DndBooleanCommand
{
    public CanAttackTarget(IGameActor character, IGameActor target) : base(character)
    {
        Target = target;
    }

    public IGameActor Target { get; set; }

    protected override void InitializeResult()
    {
        var canTakeAnyAction = new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetErrorAndReturn("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Result.Set(canTakeAnyAction);
            return;
        }

        var canBeTargeted = new CanBeTargeted(Target, Actor).Execute();

        if (!canBeTargeted.IsSuccess)
        {
            SetErrorAndReturn("CanBeTargeted: " + canBeTargeted.ErrorMessage);
            return;
        }

        if (!canBeTargeted.Value)
        {
            Result.Set(canBeTargeted);
            return;
        }

        SetValue(true, $"{Actor.Name} can attack {Target.Name}.");
    }
}
