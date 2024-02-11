namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Intimidation : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Intimidation";

    public string Description => "Influence others through threats, hostile actions, and physical violence";

    private Intimidation() { }

    public static readonly Intimidation Instance = new Intimidation();
}
