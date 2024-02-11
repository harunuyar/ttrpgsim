namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items;

public class CanEquipItem : DndBooleanCommand
{
    public CanEquipItem(Character character, IItem item) : base(character)
    {
        Item = item;
    }

    public IItem Item { get; }

    public override void InitializeResult()
    {
        if (Item.IsEquipped)
        {
            Result.SetValue("Already equiped", false);
            ShouldCollectBonuses = false;
        }
        else
        {
            Result.SetValue("Base", true);
            Item.ItemDescription.HandleCommand(this);
        }
    }
}
