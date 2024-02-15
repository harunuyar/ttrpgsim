namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

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
}
