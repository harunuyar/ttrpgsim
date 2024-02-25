namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Equipment;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class HasEquipmentProficiency : ValueCommand<bool>
{
    public HasEquipmentProficiency(IGameActor character, EquipmentModel equipment) : base(character)
    {
        Equipment = equipment;
    }

    public EquipmentModel Equipment { get; }

    protected override async Task InitializeResult()
    {
        if (Equipment.Url == null)
        {
            SetError($"{Equipment.Name} equipment url is null.");
        }

        var proficiency = await new HasProficiency(Actor, Equipment).Execute();
        if (!proficiency.IsSuccess)
        {
            SetError("HasProficiency: " + proficiency.ErrorMessage);
            return;
        }

        Set(proficiency);
    }
}
