namespace Dnd.Entities.Skills.Predefined;

using Dnd.Entities.Attributes;

public class Deception : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Deception";

    public string Description => "Convince others that something false is true";

    private Deception() { }

    public static readonly Deception Instance = new Deception();
}
