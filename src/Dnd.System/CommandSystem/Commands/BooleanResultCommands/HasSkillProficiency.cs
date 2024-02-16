﻿namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Skills;

public class HasSkillProficiency : DndBooleanCommand
{
    public HasSkillProficiency(IGameActor character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} doesn't have {Skill.Name} skill proficiency.");
    }
}