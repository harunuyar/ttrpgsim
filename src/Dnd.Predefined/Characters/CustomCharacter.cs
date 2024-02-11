namespace Dnd.Predefined.Characters;

using Dnd.Predefined.Alignments;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Races;

public class CustomCharacter : ICharacter
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
        Effects = new List<IEffect>();
    }

    public IRace Race { get; }

    public IAlignment Alignment { get; }

    public AttributeSet AttributeSet { get; }

    public LevelInfo LevelInfo { get; }

    public HitPoints HitPoints { get; }

    public Inventory Inventory { get; }

    public List<IEffect> Effects { get; }

    public bool HasInspiration { get; set; }

    public string Name { get; }
}
