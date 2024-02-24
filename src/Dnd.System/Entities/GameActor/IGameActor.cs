namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.Alignment;
using Dnd.System.Entities.Instances;

public interface IGameActor
{
    string Name { get; }

    RaceInstance Race { get; }

    SubraceInstance? Subrace { get; }

    AlignmentModel Alignment { get; }

    AbilitySet AttributeSet { get; }

    LevelInfo LevelInfo { get; }

    HitPoints HitPoints { get; }

    Inventory Inventory { get; }

    EffectsTable EffectsTable { get; }

    ActionCounter ActionCounter { get; }

    SpellMemory SpellMemory { get; }

    bool HasInspiration { get; set; }
}
