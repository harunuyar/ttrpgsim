namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;

public class CanEquipItem : DndBooleanCommand
{
    public CanEquipItem(IGameActor character, IItem item) : base(character)
    {
        Item = item;
    }

    public IItem Item { get; }

    protected override void InitializeResult()
    {
        if (!Item.ItemDescription.IsEquippable)
        {
            SetValueAndReturn(false, $"Item {Item.Name} is not equippable.");
            return;
        }
        if (Item.IsEquipped)
        {
            SetValueAndReturn(false, $"Item {Item.Name} is already equiped by someone.");
            return;
        }

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

        SetValue(true, $"{Actor.Name} can equip {Item.Name}.");
        Item.ItemDescription.HandleCommand(this);
    }
}
