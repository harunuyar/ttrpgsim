namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.DamageType;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CalculateDamage : ScoreCommand
{
    public CalculateDamage(IGameActor character, int damage, DamageTypeModel damageType) : base(character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public int Damage { get; }

    public DamageTypeModel DamageType { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(Damage, "Base damage");

        return Task.CompletedTask;
    }

    protected override async Task FinalizeResult()
    {
        var hasVulnerability = await new HasDamageVulnerability(Actor, DamageType).Execute();

        if (!hasVulnerability.IsSuccess)
        {
            SetError("HasDamageVulnerability: " + hasVulnerability.ErrorMessage);
            return;
        }

        if (hasVulnerability.Value && Result.Value > 0)
        {
            AddBonus(Result.Value, hasVulnerability.Message);
        }

        var hasResistance = await new HasDamageResistance(Actor, DamageType).Execute();

        if (!hasResistance.IsSuccess)
        {
            SetError("HasDamageResistance: " + hasResistance.ErrorMessage);
            return;
        }

        if (hasResistance.Value && Result.Value > 0)
        {
            AddBonus(-Result.Value / 2, hasResistance.Message);
        }
    }
}
