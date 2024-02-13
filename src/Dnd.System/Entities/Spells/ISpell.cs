namespace Dnd.System.Entities.Spells;

public interface ISpell : IDndEntity
{
    ISpellDescription SpellDescription { get; }
}
