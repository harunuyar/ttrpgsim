namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.GameActors;

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
    }

    protected override void FinalizeResult()
    {
        var hasVulnerability = new HasDamageVulnerability(Actor, DamageType).Execute();

        if (!hasVulnerability.IsSuccess)
        {
            SetErrorAndReturn("HasDamageVulnerability: " + hasVulnerability.ErrorMessage);
            return;
        }

        if (hasVulnerability.Value && Result.Value > 0)
        {
            AddBonus(hasVulnerability.Source ?? new CustomDndEntity("Vulnerability"), Result.Value);
        }

        var hasResistance = new HasDamageResistance(Actor, DamageType).Execute();

        if (!hasResistance.IsSuccess)
        {
            SetErrorAndReturn("HasDamageResistance: " + hasResistance.ErrorMessage);
            return;
        }

        if (hasResistance.Value && Result.Value > 0)
        {
            AddBonus(hasResistance.Source ?? new CustomDndEntity("Resistance"), -Result.Value / 2);
        }
    }
}
