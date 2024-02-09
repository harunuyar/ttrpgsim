namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetPassiveSkillValue : DndScoreCommand
{
    public GetPassiveSkillValue(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
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

    public override IntegerResultWithBonuses Execute()
    {
        return IntegerResultWithBonuses.Success(this, "Base", 10, IntegerBonuses);
    }
}
