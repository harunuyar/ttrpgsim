namespace DnD.Commands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;
using TableTopRpg.Commands;

internal class GetSkillProficiencyCommand : DndCommand
{
    public GetSkillProficiencyCommand(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        try
        {
            return SummarizedIntegerResult.Success(this, Character.GetSkillProficiency(Skill), "Base");
        }
        catch (Exception e)
        {
            return IntegerResult.Failure(this, e.Message);
        }
    }
}