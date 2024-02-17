namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;

public class GetWeaponAttackModifierAgainst : GetAttackModifierAgainst
{
    internal GetWeaponAttackModifierAgainst(IGameActor character, IItem weaponItem, IGameActor attacker) : base(character, attacker)
    {
        WeaponItem = weaponItem;
    }

    public IItem WeaponItem { get; }
}
