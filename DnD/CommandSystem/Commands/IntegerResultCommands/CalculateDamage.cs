namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Weapons;

internal class CalculateDamage : DndScoreCommand
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
