namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;

public class GetInitiativeModifier : DndScoreCommand
{
    public GetInitiativeModifier(Character character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        var getDexterityModifierCommand = new GetAttributeModifier(Character, EAttributeType.Dexterity);
        var dexterityModifierResult = getDexterityModifierCommand.Execute();

        if (dexterityModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.Dexterity, dexterityModifierResult.Value);
        }
        else
        {
            Result.SetError(dexterityModifierResult.ErrorMessage ?? "Unknown");
        }
    }
}
