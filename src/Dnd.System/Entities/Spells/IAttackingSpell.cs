namespace Dnd.System.Entities.Spells;

using Dnd.System.Entities.Actions;

public interface IAttackingSpell : ISpell, IAttackAction
{
    int ProjectileCount { get; }
}
