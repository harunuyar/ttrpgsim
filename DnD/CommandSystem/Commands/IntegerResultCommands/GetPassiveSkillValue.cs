namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetPassiveSkillValue : DndScoreCommand
{
    public GetPassiveSkillValue(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Base", 10);

        var getSkillModifierCommand = new GetSkillModifier(Character, Skill);
        var skillModifierResult = getSkillModifierCommand.Execute();

        if (skillModifierResult.IsSuccess)
        {
            Result.BonusCollection.AddBonus(Skill, skillModifierResult.Value);
        }
    }
}
