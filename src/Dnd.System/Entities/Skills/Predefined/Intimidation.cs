namespace Dnd.Entities.Skills.Predefined;

using Dnd.Entities.Attributes;

public class Intimidation : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Intimidation";

    public string Description => "Influence others through threats, hostile actions, and physical violence";

    private Intimidation() { }

    public static readonly Intimidation Instance = new Intimidation();
}
