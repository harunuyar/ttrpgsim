namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Deception : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Deception";

    public string Description => "Convince others that something false is true";

    private Deception() { }

    public static readonly Deception Instance = new Deception();
}
