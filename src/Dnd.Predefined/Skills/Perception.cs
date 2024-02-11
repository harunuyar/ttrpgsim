namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Perception : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Wisdom;

    public string Name => "Perception";

    public string Description => "Your Wisdom (Perception) check lets you spot, hear, or otherwise detect the presence of something. It measures your general awareness of your surroundings and the keenness of your senses.";

    private Perception() { }

    public static readonly Perception Instance = new Perception();
}