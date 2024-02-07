namespace DnD.Entities.Characters;

using DnD.Entities.Attributes;
using DnD.Entities.Races;
using DnD.Entities.Skills;
using DnD.GameManagers.Dice;
using TableTopRpg.Entities;
using TableTopRpg.Entities.Character;

internal class Player : IPlayer
{
    public Player(string name, Race race)
    {
        this.Race = race;
        this.Name = name;

        this.Alignment = Allignments.Alignment.None;
        this.AttributeSet = new AttributeSet();
        this.HitPoints = new HitPoints();
        this.SkillProficiencies = new Dictionary<Skill, int>();
        this.Traits = new List<ITrait>();
        this.Levels = new List<Level>();
    }

    public string Name { get; set; }
    public IRace Race { get; set; }
    public IAlignment Alignment { get; set; }
    public Dictionary<Skill, int> SkillProficiencies { get; }
    public List<ITrait> Traits { get; }
    public List<Level> Levels { get; }
    public int Level => Levels.Sum(l => l.LevelNum);
    public HitPoints HitPoints { get; }
    public AttributeSet AttributeSet { get; }

    public void SetSkillProficiency(Skill skill, int proficiencyLevel)
    {
        SkillProficiencies[skill] = proficiencyLevel;
    }

    public int GetSkillProficiency(Skill skill)
    {
        return SkillProficiencies.GetValueOrDefault(skill, 0);
    }
}
