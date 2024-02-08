namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Religion : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Intelligence;

    public string Name => "Religion";

    public string Description => "Your Intelligence (Religion) check measures your ability to recall lore about deities, rites and prayers, religious hierarchies, holy symbols, and the practices of secret cults.";

    private Religion() { }

    public static readonly Religion Instance = new Religion();
}
