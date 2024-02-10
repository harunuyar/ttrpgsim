namespace Dnd.Entities.Characters;

using Dnd.Entities.Allignments;
using Dnd.Entities.Effects;
using Dnd.Entities.Feats;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Races;
using Dnd.Entities.Skills;
using Dnd.Entities.Traits;

public class Character
{
    public Character(string name, IRace race)
    {
        this.Race = race;
        this.Name = name;

        this.Alignment = Allignments.Alignment.None;
        this.AttributeSet = new AttributeSet();
        this.HitPoints = new HitPoints();
        this.SkillsWithProficiency = new HashSet<IDndSkill>();
        this.ArmorProficiencies = EArmorType.None;
        this.WeaponProficiencies = EWeaponType.None;
        this.Traits = new List<ATrait>();
        this.Feats = new List<AFeat>();
        this.Levels = new List<Level>();
        this.Inventory = new Inventory();
        this.Effects = new List<AEffect>();
    }

    public string Name { get; set; }

    public IRace Race { get; set; }

    public IAlignment Alignment { get; set; }

    public HashSet<IDndSkill> SkillsWithProficiency { get; }

    public EArmorType ArmorProficiencies { get; set; }

    public EWeaponType WeaponProficiencies { get; set; }

    public List<ATrait> Traits { get; }

    public List<AFeat> Feats { get; }

    public List<Level> Levels { get; }

    public int Level => Levels.Sum(l => l.LevelNum);

    public AttributeSet AttributeSet { get; }

    public HitPoints HitPoints { get; }

    public Inventory Inventory { get; }

    public List<AEffect> Effects { get; }

    public bool HasInspiration { get; set; }

    public void SetSkillProficiency(IDndSkill skill, bool isProficient)
    {
        if (isProficient)
        {
            SkillsWithProficiency.Add(skill);
        }
        else
        {
            SkillsWithProficiency.Remove(skill);
        }
    }

    public bool GetSkillProficiency(IDndSkill skill)
    {
        return SkillsWithProficiency.Contains(skill);
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

    public void SetWeaponProficiency(EWeaponType weaponType, bool isProficient)
    {
        if (isProficient)
        {
            WeaponProficiencies |= weaponType;
        }
        else
        {
            WeaponProficiencies &= ~weaponType;
        }
    }

    public bool HasWeaponProficiency(EWeaponType weaponType)
    {
        return WeaponProficiencies.HasFlag(weaponType) || Levels.Any(l => l.Class.WeaponProficiencies.HasFlag(weaponType));
    }
}
