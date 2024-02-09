namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities;
using DnD.Entities.Characters;
using DnD.Entities.Skills;
using DnD.GameManagers.Dice;

internal class MakeSkillCheckRoll : DndRollCommand
{
    public MakeSkillCheckRoll(Character character, IDndSkill Skill, EAdvantage? overrideAdvantage = null) : base(character, EDiceType.D20, overrideAdvantage)
    {
        this.Skill = Skill;
    }

    public IDndSkill Skill { get; }

    override public void CollectBonuses()
    {
        var getSkillModifierCommand = new GetSkillModifier(Character, Skill);
        getSkillModifierCommand.CollectBonuses();
        var skillModifierResult = getSkillModifierCommand.Execute();

        if (skillModifierResult.IsSuccess)
        {
            IntegerBonuses.AddBonus(Skill.Name, skillModifierResult.Value);
        }

        base.CollectBonuses();
    }
}
