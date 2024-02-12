﻿namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Skills;

public class GetSkillModifier : DndScoreCommand
{
    public GetSkillModifier(ICharacter character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    public override void InitializeResult()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, Skill.AttributeType);
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.GetAttribute(Skill.AttributeType), attributeModifierResult.Value);
        }
        else
        {
            Result.SetError(attributeModifierResult.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }
}