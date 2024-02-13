namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Skills;

public class GetPassiveSkillValue : DndScoreCommand
{
    public GetPassiveSkillValue(ICharacter character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 10);

        var getSkillModifierCommand = new GetSkillModifier(Character, Skill);
        var skillModifierResult = getSkillModifierCommand.Execute();

        if (skillModifierResult.IsSuccess)
        {
            Result.BonusCollection.AddBonus(Skill, skillModifierResult.Value);
        }
    }

    protected override void FinalizeResult()
    {
    }
}
