namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class GetDamageResistance : DndBooleanCommand
{
    public GetDamageResistance(Character character, EDamageType damageType) : base(character)
    {
    }

    public EDamageType DamageType { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base", false);
    }
}
