namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.Predefined.Feats;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

public class Defense : AFeat, IFightingStyle
{
    public static readonly Defense Instance = new Defense();

    private Defense() : base("Fighting Style: Defense", "While you are wearing armor, you gain a +1 bonus to AC.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetArmorClass getArmorClass && getArmorClass.Actor.Inventory.Equipments.Armor != null)
        {
            getArmorClass.AddBonus(this, 1);
        }
    }
}
