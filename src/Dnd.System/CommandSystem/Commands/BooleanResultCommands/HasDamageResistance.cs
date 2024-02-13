
namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class HasDamageResistance : DndBooleanCommand
{
    public HasDamageResistance(ICharacter character, EDamageType damageType) : base(character)
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
