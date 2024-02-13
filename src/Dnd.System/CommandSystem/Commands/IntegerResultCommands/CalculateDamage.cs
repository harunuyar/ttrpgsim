namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CalculateDamage : DndScoreCommand
{
    public CalculateDamage(IGameActor character, int damage, EDamageType damageType) : base(character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public int Damage { get; }

    public EDamageType DamageType { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Damage", Damage);

        var getDamageResistance = new HasDamageResistance(Character, DamageType);
        var damageResistanceResult = getDamageResistance.Execute();

        if (damageResistanceResult.IsSuccess && damageResistanceResult.Value)
        {
            Result.BonusCollection.AddBonus(damageResistanceResult.Source ?? new CustomDndEntity("Resistance"), -Damage / 2);
        }
    }

    protected override void FinalizeResult()
    {
    }
}
