namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Performance : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Charisma;

    public string Name => "Performance";

    public string Description => "Your Charisma (Performance) check determines how well you can delight an audience with music, dance, acting, storytelling, or some other form of entertainment.";

    private Performance() { }

    public static readonly Performance Instance = new Performance();
}