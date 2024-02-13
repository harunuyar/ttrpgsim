namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;

public class CalculateWeaponAttackModifierAgainst : DndScoreCommand
{
    public CalculateWeaponAttackModifierAgainst(IGameActor character, IItem weaponItem, IGameActor attacker) : base(character)
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

    protected override void FinalizeResult()
    {
    }
}
