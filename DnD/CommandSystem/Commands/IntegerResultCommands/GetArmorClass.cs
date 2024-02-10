namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetArmorClass : DndScoreCommand
{
    public GetArmorClass(Character character) : base(character)
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
