namespace Dnd.Predefined.Characters;

using Dnd.Predefined.Alignments;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Feats;
using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;

public class CustomCharacter : ICharacter
{
    public CustomCharacter(string name, IRace race, IAlignment? alignment = null)
    {
        Name = name;
        Race = race;
        Alignment = alignment ?? None.Instance;
        AttributeSet = new AttributeSet();
        Traits = new List<ITrait>();
        Feats = new List<IFeat>();
        Levels = new List<Level>();
        HitPoints = new HitPoints();
        Inventory = new Inventory();
        Effects = new List<IEffect>();
    }

    public IRace Race { get; }

    public IAlignment Alignment { get; }

    public AttributeSet AttributeSet { get; }

    public List<ITrait> Traits { get; }

    public List<IFeat> Feats { get; }

    public List<Level> Levels { get; }

    public HitPoints HitPoints { get; }

    public Inventory Inventory { get; }

    public List<IEffect> Effects { get; }

    public bool HasInspiration { get; set; }

    public string Name { get; }
}
