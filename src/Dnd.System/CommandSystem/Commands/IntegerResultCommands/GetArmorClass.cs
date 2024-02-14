namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetArmorClass : DndScoreCommand
{
    public GetArmorClass(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base Armor Class", 10);

        var getDexterityModifier = new GetAttributeModifier(Actor, EAttributeType.Dexterity);
        var dexterityModifierResult = getDexterityModifier.Execute();

        if (dexterityModifierResult.IsSuccess)
        {
            Result.BonusCollection.AddBonus(Actor.AttributeSet.Dexterity, dexterityModifierResult.Value);
        }
    }
}
