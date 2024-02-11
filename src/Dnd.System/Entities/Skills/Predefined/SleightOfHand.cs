namespace Dnd.Entities.Skills.Predefined;

using Dnd.Entities.Attributes;

public class SleightOfHand : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Dexterity;

    public string Name => "Sleight Of Hand";

    public string Description => "Whenever you attempt an act of legerdemain or manual trickery, such as planting something on someone else or concealing an object on your person, make a Dexterity (Sleight of Hand) check. The check might also apply to pickpocketing, palming objects, and concealing objects on your person.";

    private SleightOfHand() { }

    public static readonly SleightOfHand Instance = new SleightOfHand();
}
