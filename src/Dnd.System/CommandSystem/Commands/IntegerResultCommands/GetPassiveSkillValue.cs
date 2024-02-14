namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Skills;

public class GetPassiveSkillValue : DndScoreCommand
{
    public GetPassiveSkillValue(IGameActor character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 10);

        var getSkillModifierCommand = new GetSkillModifier(Actor, Skill);
        var skillModifierResult = getSkillModifierCommand.Execute();

        if (!skillModifierResult.IsSuccess)
        {
            SetErrorAndReturn("GetSkillModifier: " + skillModifierResult.ErrorMessage);
            return;
        }

        Result.BonusCollection.AddBonus(Skill, skillModifierResult.Value);
    }
}
