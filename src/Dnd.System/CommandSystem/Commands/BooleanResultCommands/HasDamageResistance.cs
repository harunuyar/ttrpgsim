
namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class HasDamageResistance : DndBooleanCommand
{
    public HasDamageResistance(Character character, EDamageType damageType) : base(character)
    {
        DamageType = damageType;
    }

    public EDamageType DamageType { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base", false);
    }
}
