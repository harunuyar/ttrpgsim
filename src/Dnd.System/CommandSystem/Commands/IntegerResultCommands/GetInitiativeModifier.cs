namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;

public class GetInitiativeModifier : DndScoreCommand
{
    public GetInitiativeModifier(ICharacter character) : base(character)
    {
    }

    protected override void InitializeResult()
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

    protected override void FinalizeResult()
    {
    }
}
