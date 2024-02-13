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
            Result.SetValue("Already equiped", false);
            ShouldVisitEntities = false;
        }
        else
        {
            Result.SetValue("Base", true);
            Item.ItemDescription.HandleCommand(this);
        }
    }

    protected override void FinalizeResult()
    {
    }
}
