namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetInitiativeModifier : DndScoreCommand
{
    public GetInitiativeModifier(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base Initiative", 0);

        var dexterityModifierResult = new GetAttributeModifier(Actor, EAttributeType.Dexterity).Execute();

        if (!dexterityModifierResult.IsSuccess)
        {
            Result.SetError("GetAttributeModifier: " + dexterityModifierResult.ErrorMessage);
            return;
        }

        Result.AddAsBonus(Actor.AttributeSet.Dexterity, dexterityModifierResult);
    }
}
