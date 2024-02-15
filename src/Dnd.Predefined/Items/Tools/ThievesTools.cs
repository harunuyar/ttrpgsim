namespace Dnd.Predefined.Items.Tools;

using Dnd.System.Entities.Items.Tools;
using Dnd.System.Entities.Units;

public class ThievesTools : ATool
{
    public ThievesTools() : base("Thieves' Tools", "A set of tools used to pick locks and disarm traps.", EToolType.Thieves, Weight.OfPounds(1), Value.OfGold(25))
    {
    }
}
