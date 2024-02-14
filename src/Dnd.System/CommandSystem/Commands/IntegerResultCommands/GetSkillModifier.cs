namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Skills;

public class GetSkillModifier : DndScoreCommand
{
    public GetSkillModifier(IGameActor character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Actor, Skill.AttributeType);
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (!attributeModifierResult.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + attributeModifierResult.ErrorMessage);
            return;
        }

        Result.SetBaseValue(Actor.AttributeSet.GetAttribute(Skill.AttributeType), attributeModifierResult.Value);
    }
}