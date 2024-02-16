
namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Damage;
using Dnd.System.Entities.GameActors;

public class HasDamageResistance : DndBooleanCommand
{
    public HasDamageResistance(IGameActor character, EDamageType damageType) : base(character)
    {
        DamageType = damageType;
    }

    public EDamageType DamageType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} doesn't have resistance to {DamageType}");
    }
}
