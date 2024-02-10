namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class CalculateDamage : DndScoreCommand
{
    public CalculateDamage(Character character, int damage, EDamageType damageType) : base(character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public int Damage { get; }

    public EDamageType DamageType { get; }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Damage", Damage);
    }
}
