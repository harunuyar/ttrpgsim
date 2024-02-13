namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetConcentrationSavingThrowModifier : GetSavingThrowModifier
{
    public GetConcentrationSavingThrowModifier(IGameActor character, EDamageType damageType) : base(character, EAttributeType.Constitution, damageType)
    {
    }
}
