namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;

public class HasArmorProficiency : DndBooleanCommand
{
    public HasArmorProficiency(IGameActor character, EArmorType armorType) : base(character)
    {
        ArmorType = armorType;
    }

    public EArmorType ArmorType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}
