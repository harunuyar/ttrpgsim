namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Performance : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Performance";

    public string Description => "Your Charisma (Performance) check determines how well you can delight an audience with music, dance, acting, storytelling, or some other form of entertainment.";

    private Performance() { }

    public static readonly Performance Instance = new Performance();
}