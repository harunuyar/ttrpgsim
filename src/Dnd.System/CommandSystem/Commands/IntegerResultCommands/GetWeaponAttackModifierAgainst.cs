namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;

public class GetWeaponAttackModifierAgainst : DndScoreCommand
{
    public GetWeaponAttackModifierAgainst(IGameActor character, IItem weaponItem, IGameActor attacker) : base(character)
    {
        WeaponItem = weaponItem;
        Attacker = attacker;
    }

    public IItem WeaponItem { get; }

    public IGameActor Attacker { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
