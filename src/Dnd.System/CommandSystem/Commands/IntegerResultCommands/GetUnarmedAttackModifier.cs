namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetUnarmedAttackModifier : DndScoreCommand
{
    public GetUnarmedAttackModifier(IGameActor character, IGameActor target) : base(character)
    {
        Target = target;
    }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);

        var strengthModifier = new GetAttributeModifier(Actor, EAttributeType.Strength).Execute();

        if (!strengthModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
            return;
        }

        IAttribute usedAttribute = Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
        var attributeModifier = strengthModifier;

        var dexterityModifier = new GetAttributeModifier(Actor, EAttributeType.Dexterity).Execute();

        if (!dexterityModifier.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
            return;
        }

        if (dexterityModifier.Value > attributeModifier.Value)
        {
            attributeModifier = dexterityModifier;
            usedAttribute = Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
        }

        Result.AddAsBonus(usedAttribute, attributeModifier);

        var proficiencyBonus = new GetProficiencyBonus(Actor).Execute();

        if (!proficiencyBonus.IsSuccess)
        {
            SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
            return;
        }

        Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
    }
}
