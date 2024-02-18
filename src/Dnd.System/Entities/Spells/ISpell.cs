namespace Dnd.System.Entities.Spells;

using Dnd.System.Entities.Actions;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Units;

public interface ISpell : IDndEntity
{
    string Description { get; }

    int Level { get; }

    TimeSpan CastingTime { get; }

    ESpellComponent SpellComponents { get; }

    Range Range { get; }

    EActionType ActionType { get; }

    void ApplyEffect(IGameActor caster, IEnumerable<IGameActor> targets);
}
