namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetInitiativeModifier : DndScoreCommand
{
    public GetInitiativeModifier(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        var getDexterityModifierCommand = new GetAttributeModifier(Actor, EAttributeType.Dexterity);
        var dexterityModifierResult = getDexterityModifierCommand.Execute();

        if (!dexterityModifierResult.IsSuccess)
        {
            Result.SetError("GetAttributeModifier: " + dexterityModifierResult.ErrorMessage);
            return;
        }

        Result.SetBaseValue(Actor.AttributeSet.Dexterity, dexterityModifierResult.Value);
    }
}
