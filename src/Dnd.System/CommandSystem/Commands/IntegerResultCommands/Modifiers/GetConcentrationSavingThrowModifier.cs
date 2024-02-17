namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.GameActors;

public class GetConcentrationSavingThrowModifier : GetSavingThrowModifier
{
    public GetConcentrationSavingThrowModifier(IGameActor character, EDamageType damageType) : base(character, EAttributeType.Constitution, damageType)
    {
    }
}
