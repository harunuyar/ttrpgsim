namespace Dnd.Predefined.Characters;

using Dnd.Predefined.Alignments;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Races;

public class CustomCharacter : IGameActor
{
    public CustomCharacter(string name, IRace race, IAlignment? alignment = null)
    {
        Name = name;
        Race = race;
        Alignment = alignment ?? None.Instance;
        AttributeSet = new AttributeSet();
        LevelInfo = new LevelInfo();
        HitPoints = new HitPoints();
        Inventory = new Inventory();
        EffectsTable = new EffectsTable();
    }

    public IRace Race { get; }

    public IAlignment Alignment { get; }

    public AttributeSet AttributeSet { get; }

    public LevelInfo LevelInfo { get; }

    public HitPoints HitPoints { get; }

    public Inventory Inventory { get; }

    public EffectsTable EffectsTable { get; }

    public bool HasInspiration { get; set; }

    public string Name { get; }
}
