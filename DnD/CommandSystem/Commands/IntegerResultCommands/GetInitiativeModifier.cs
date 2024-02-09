namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetInitiativeModifier : DndScoreCommand
{
    public GetInitiativeModifier(Character character) : base(character)
    {
    }

    public override IntegerResultWithBonuses Execute()
    {
        var getDexterityModifierCommand = new GetAttributeModifier(Character, EAttributeType.Dexterity);
        getDexterityModifierCommand.CollectBonuses();
        var dexterityModifierResult = getDexterityModifierCommand.Execute();

        if (dexterityModifierResult.IsSuccess)
        {
            return IntegerResultWithBonuses.Success(this, "Dexterity", dexterityModifierResult.Value, IntegerBonuses);
        }

        return IntegerResultWithBonuses.Failure(this, "Dexterity modifier not found");
    }
}
