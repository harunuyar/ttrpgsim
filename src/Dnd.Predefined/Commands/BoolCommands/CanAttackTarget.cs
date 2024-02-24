namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanAttackTarget : ValueCommand<bool>
{
    public CanAttackTarget(IGameActor character, IGameActor target) : base(character)
    {
        Target = target;
    }

    public IGameActor Target { get; set; }

    protected override async Task InitializeResult()
    {
        var canTakeAnyAction = await new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetError("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Set(canTakeAnyAction);
            ForceComplete();
            return;
        }

        var canBeTargeted = await new CanBeTargeted(Target, Actor).Execute();

        if (!canBeTargeted.IsSuccess)
        {
            SetError("CanBeTargeted: " + canBeTargeted.ErrorMessage);
            return;
        }

        if (!canBeTargeted.Value)
        {
            Set(canBeTargeted);
            return;
        }

        SetValue(true, $"{Actor.Name} can attack {Target.Name}.");
    }
}
