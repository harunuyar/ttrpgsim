namespace Dnd.System.Entities.Characters;

using Dnd.System.Entities;
using Dnd.System.Entities.Allignments;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Races;

public interface ICharacter : IDndEntity
{
    IRace Race { get; }

    IAlignment Alignment { get; }

    AttributeSet AttributeSet { get; }

    LevelInfo LevelInfo { get; }

    HitPoints HitPoints { get; }

    Inventory Inventory { get; }

    List<IEffect> Effects { get; }

    bool HasInspiration { get; set; }
}
