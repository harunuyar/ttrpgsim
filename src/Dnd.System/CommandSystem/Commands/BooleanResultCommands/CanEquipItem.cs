namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

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
        if (Item.IsEquipped)
        {
            SetValue(false, "Item is already equiped by someone.");
            ForceComplete();
        }
        else
        {
            SetValue(true, "By default, you can equip this item.");
            Item.ItemDescription.HandleCommand(this);
        }
    }
}
