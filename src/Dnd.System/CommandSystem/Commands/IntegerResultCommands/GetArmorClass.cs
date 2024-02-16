namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
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

        var dexterityModifierResult = new GetAttributeModifier(Actor, EAttributeType.Dexterity).Execute();

        if (!dexterityModifierResult.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + dexterityModifierResult.ErrorMessage);
        }

        Result.AddAsBonus("Dexterity Modifier", dexterityModifierResult);
    }
}
