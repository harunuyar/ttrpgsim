namespace Dnd.Predefined.Feats.Fighter;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

public class FightingStyleDefense : AFeat
{
    public static readonly FightingStyleDefense Instance = new FightingStyleDefense();

    private FightingStyleDefense() : base("Fighting Style: Defense", "While you are wearing armor, you gain a +1 bonus to AC.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        base.HandleCommand(command);

        if (command is GetArmorClass getArmorClass && getArmorClass.Character.Inventory.Equipments.Armor != null)
        {
            getArmorClass.Result.BonusCollection.AddBonus(this, 1);
        }
    }
}
