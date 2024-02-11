namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Arcana : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Intelligence;

    public string Name => "Arcana";

    public string Description => "Your Intelligence (Arcana) check measures your ability to recall lore about spells, magic items, eldritch symbols, magical traditions, the planes of existence, and the inhabitants of those planes.";

    private Arcana() { }

    public static readonly Arcana Instance = new Arcana();
}
