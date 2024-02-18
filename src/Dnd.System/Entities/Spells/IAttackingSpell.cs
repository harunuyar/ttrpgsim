namespace Dnd.System.Entities.Spells;

using Dnd.System.Entities.Actions;

public interface IAttackingSpell : ISpell, IAttacking
{
    int ProjectileCount { get; }
}
