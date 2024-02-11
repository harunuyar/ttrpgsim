namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items;

public class CanEquipItem : DndBooleanCommand
{
    public CanEquipItem(ICharacter character, IItem item) : base(character)
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
