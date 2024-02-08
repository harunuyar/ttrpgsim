namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Survival : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Wisdom;

    public string Name => "Survival";

    public string Description => "The DM might ask you to make a Wisdom (Survival) check to follow tracks, hunt wild game, guide your group through frozen wastelands, identify signs that owlbears live nearby, predict the weather, or avoid quicksand and other natural hazards.";

    private Survival() { }

    public static readonly Survival Instance = new Survival();
}
