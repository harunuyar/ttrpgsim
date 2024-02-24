namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Skill;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActor;

public class HasSkillExpertise : ValueCommand<bool>
{
    public HasSkillExpertise(IGameActor character, SkillModel skill) : base(character)
    {
        Skill = skill;
    }

    public SkillModel Skill { get; }

    protected override async Task InitializeResult()
    {
        if (Skill.Url == null)
        {
            SetError($"{Skill.Name} skill url is null.");
            return;
        }

        foreach (var p in Actor.LevelInfo.GetLevels().SelectMany(l => l.Features).SelectMany(f => f.ExpertiseOptions))
        {
            if (await p.HasProficiency(Skill.Url))
            {
                SetValue(true, $"{Actor.Name} has {Skill} skill expertise.");
                return;
            }
        }

        SetValue(false, $"{Actor.Name} doesn't have {Skill} skill expertise.");
    }
}