namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.Skill;
using Dnd.Predefined.Commands.BonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetPassiveSkillValue : ScoreCommand
{
    public GetPassiveSkillValue(IGameActor character, SkillModel skill) : base(character)
    {
        Skill = skill;
    }

    public SkillModel Skill { get; }

    protected override async Task InitializeResult()
    {
        SetBaseValue(10, "Base");

        var skillModifierResult = await new GetSkillModifier(Actor, Skill).Execute();

        if (!skillModifierResult.IsSuccess)
        {
            SetError("GetSkillModifier: " + skillModifierResult.ErrorMessage);
            return;
        }

        AddBonus(skillModifierResult);
    }
}
