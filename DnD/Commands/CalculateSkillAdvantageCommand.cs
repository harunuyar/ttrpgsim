namespace DnD.Commands;

using DnD.Entities;
using DnD.Entities.Characters;
using DnD.Entities.Skills;
using TableTopRpg.Commands;

internal class CalculateSkillAdvantageCommand : DndCommand
{
    public CalculateSkillAdvantageCommand(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill? Skill { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        return IntegerResult.Success(this, (int)EAdvantage.None);
    }
}
