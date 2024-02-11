namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;

public class GetArmorClass : DndScoreCommand
{
    public GetArmorClass(ICharacter character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Base Armor Class", 10);

        var getDexterityModifier = new GetAttributeModifier(Character, EAttributeType.Dexterity);
        var dexterityModifierResult = getDexterityModifier.Execute();

        if (dexterityModifierResult.IsSuccess)
        {
            Result.BonusCollection.AddBonus(Character.AttributeSet.Dexterity, dexterityModifierResult.Value);
        }
    }
}
