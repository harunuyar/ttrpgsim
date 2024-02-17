namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;

public class CanAttackTarget : DndBooleanCommand
{
    public CanAttackTarget(IGameActor character, IItem weaponItem, IGameActor target) : base(character)
    {
        WeaponItem = weaponItem;
        Target = target;
    }

    public IItem WeaponItem { get; set; }

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

        SetValue(true, $"{Actor.Name} can attack {Target.Name}.");
    }
}
