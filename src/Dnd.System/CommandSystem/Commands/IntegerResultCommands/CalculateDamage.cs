namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CalculateDamage : DndScoreCommand
{
    public CalculateDamage(ICharacter character, int damage, EDamageType damageType) : base(character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public int Damage { get; }

    public EDamageType DamageType { get; }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Damage", Damage);

        var getDamageResistance = new HasDamageResistance(Character, DamageType);
        var damageResistanceResult = getDamageResistance.Execute();

        if (damageResistanceResult.IsSuccess && damageResistanceResult.Value)
        {
            Result.BonusCollection.AddBonus(damageResistanceResult.Source ?? new CustomDndEntity("Resistance"), -Damage / 2);
        }
    }
}
