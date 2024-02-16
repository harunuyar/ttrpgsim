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
        if (!Item.ItemDescription.IsEquippable)
        {
            SetValue(false, $"Item {Item.Name} is not equippable.");
        }
        if (Item.IsEquipped)
        {
            SetValue(false, $"Item {Item.Name} is already equiped by someone.");
            ForceComplete();
        }
        else
        {
            SetValue(true, $"{Actor.Name} can equip {Item.Name}.");
            Item.ItemDescription.HandleCommand(this);
        }
    }
}
