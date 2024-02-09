﻿namespace DnD.Entities.Characters;

using DnD.Entities.Effects;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Races;
using DnD.Entities.Skills;
using TableTopRpg.Entities;
using TableTopRpg.Entities.Character;

internal class Character : IGameActor
{
    public Character(string name, Race race)
    {
        this.Race = race;
        this.Name = name;

        this.Alignment = Allignments.Alignment.None;
        this.AttributeSet = new AttributeSet();
        this.HitPoints = new HitPoints();
        this.SkillProficiencies = new Dictionary<IDndSkill, int>();
        this.ArmorProficiencies = EArmorType.None;
        this.Traits = new List<ITrait>();
        this.Feats = new List<IFeat>();
        this.Levels = new List<Level>();
        this.Inventory = new Inventory();
        this.Effects = new List<IEffect>();
    }

    public string Name { get; set; }
    public IRace Race { get; set; }
    public IAlignment Alignment { get; set; }
    public Dictionary<IDndSkill, int> SkillProficiencies { get; }
    public EArmorType ArmorProficiencies { get; set; }  
    public List<ITrait> Traits { get; }
    public List<IFeat> Feats { get; }
    public List<Level> Levels { get; }
    public int Level => Levels.Sum(l => l.LevelNum);
    public AttributeSet AttributeSet { get; }
    public HitPoints HitPoints { get; }
    public Inventory Inventory { get; }
    public List<IEffect> Effects { get; }
    public bool HasInspiration { get; set; }


    public void SetSkillProficiency(IDndSkill skill, int proficiencyLevel)
    {
        SkillProficiencies[skill] = proficiencyLevel;
    }

    public int GetSkillProficiency(IDndSkill skill)
    {
        return SkillProficiencies.GetValueOrDefault(skill, 0);
    }

    public void SetArmorProficiency(EArmorType armorType, bool isProficient)
    {
        if (isProficient)
        {
            ArmorProficiencies |= armorType;
        }
        else
        {
            ArmorProficiencies &= ~armorType;
        }
    }

    public bool HasArmorProficiency(EArmorType armorType)
    {
        return ArmorProficiencies.HasFlag(armorType) || Levels.Any(l => l.Class.ArmorProficiencies.HasFlag(armorType));
    }
}