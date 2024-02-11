namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Acrobatics : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Dexterity;

    public string Name => "Acrobatics";

    public string Description => "Performing acrobatic feats such as jumps or rolls";

    private Acrobatics() { }

    public static readonly Acrobatics Instance = new Acrobatics();
}
