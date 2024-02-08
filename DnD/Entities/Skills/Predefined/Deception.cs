namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Deception : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Deception";

    public string Description => "Convince others that something false is true";

    private Deception() { }

    public static readonly Deception Instance = new Deception();
}
