namespace Dnd.System.Entities.Spells;

using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Units;

public interface ISpell : IAction
{
    int Level { get; }

    TimeSpan CastingTime { get; }

    ESpellComponent SpellComponents { get; }
}
