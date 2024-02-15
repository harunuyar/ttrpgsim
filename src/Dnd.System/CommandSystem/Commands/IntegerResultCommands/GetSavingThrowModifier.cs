namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.GameActors;

public class GetSavingThrowModifier : DndScoreCommand
{
    public GetSavingThrowModifier(IGameActor character, EAttributeType attributeType, EDamageType damageType) : base(character)
    {
        AttributeType = attributeType;
        DamageType = damageType;
    }

    public EAttributeType AttributeType { get; }

    public EDamageType DamageType { get; }

    protected override void InitializeResult()
    {
        var attributeModifierResult = new GetAttributeModifier(Actor, AttributeType).Execute();

        if (!attributeModifierResult.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + attributeModifierResult.ErrorMessage);
            return;
        }

        Result.SetBaseValue(Actor.AttributeSet.GetAttribute(AttributeType), attributeModifierResult.Value);

        var hasSavingThrowProficiency = new HasSavingThrowProficiency(Actor, AttributeType).Execute();

        if (!hasSavingThrowProficiency.IsSuccess)
        {
            SetErrorAndReturn("HasSavingThrowProficiency: " + hasSavingThrowProficiency.ErrorMessage);
        }

        if (hasSavingThrowProficiency.Value)
        {
            var proficiencyBonusResult = new GetProficiencyBonus(Actor).Execute();

            if (!proficiencyBonusResult.IsSuccess)
            {
                SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonusResult.ErrorMessage);
            }

            Result.AddAsBonus("Proficiency Bonus", proficiencyBonusResult);
        }
    }
}
