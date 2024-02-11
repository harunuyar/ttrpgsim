namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Persuasion : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Persuasion";

    public string Description => "Your Charisma (Persuasion) check lets you persuade others to do something or to believe something.";

    private Persuasion() { }

    public static readonly Persuasion Instance = new Persuasion();
}
