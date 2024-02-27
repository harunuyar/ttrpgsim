namespace Dnd.System.Entities.GameActor;

using Dnd.System.Entities.Instances;

public interface IGameActor : ICommandHandler
{
    string Name { get; }
    bool HasInspiration { get; set; }
    IRaceInstance Race { get; }
    ISubraceInstance? Subrace { get; }
    IAlignmentInstance? Alignment { get; }
    IAbilitySet AttributeSet { get; }
    ILevelInfo LevelInfo { get; }
    IHitPoints HitPoints { get; }
    IEffectsTable EffectsTable { get; }
    IInventory Inventory { get; }
    IActionCounter ActionCounter { get; }
    ISpellMemory SpellMemory { get; }
}
