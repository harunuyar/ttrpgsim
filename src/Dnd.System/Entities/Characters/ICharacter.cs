namespace Dnd.System.Entities.Characters;

using Dnd.System.Entities;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Feats;
using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;

public interface ICharacter : IDndEntity
{
    public IRace Race { get; }

    public IAlignment Alignment { get; }

    public AttributeSet AttributeSet { get; }

    public List<ITrait> Traits { get; }

    public List<IFeat> Feats { get; }

    public List<Level> Levels { get; }

    public int Level => Levels.Sum(l => l.LevelNum);

    public HitPoints HitPoints { get; }

    public Inventory Inventory { get; }

    public List<IEffect> Effects { get; }

    public bool HasInspiration { get; set; }
}
