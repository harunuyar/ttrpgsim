namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class AnimalHandling : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Wisdom;

    public string Name => "Animal Handling";

    public string Description => "The ability to calm down or train animals";

    private AnimalHandling() { }

    public static readonly AnimalHandling Instance = new AnimalHandling();
}
