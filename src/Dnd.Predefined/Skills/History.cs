namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class History : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Intelligence;

    public string Name => "History";

    public string Description => "Your Intelligence (History) check measures your ability to recall lore about historical events, legendary people, ancient kingdoms, past disputes, recent wars, and lost civilizations.";

    private History() { }

    public static readonly History Instance = new History();
}
