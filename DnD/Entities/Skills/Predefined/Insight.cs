namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Insight : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Wisdom;

    public string Name => "Insight";

    public string Description => "Your Wisdom (Insight) check decides whether you can determine the true intentions of a creature, such as when searching out a lie or predicting someone’s next move.";

    private Insight() { }

    public static readonly Insight Instance = new Insight();
}
