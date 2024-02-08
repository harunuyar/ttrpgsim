namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Investigation : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Intelligence;

    public string Name => "Investigation";

    public string Description => "Deduce the location of a hidden object, discern from the appearance of a wound what kind of weapon dealt it, or determine the weakest point in a tunnel that could cause it to collapse";

    private Investigation() { }

    public static readonly Investigation Instance = new Investigation();
}
