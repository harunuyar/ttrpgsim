
namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class HasDamageResistance : DndBooleanCommand
{
    public HasDamageResistance(IGameActor character, EDamageType damageType) : base(character)
    {
        DamageType = damageType;
    }

    public EDamageType DamageType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue("Base", false);
    }

    protected override void FinalizeResult()
    {
    }
}
