namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Ranger : IDndClass
{
    public EDiceType HitDie => EDiceType.d10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Dexterity;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Ranger";

    public string Description => "A warrior who combats threats on the edges of civilization";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        AnimalHandling.Instance,
        Athletics.Instance,
        Insight.Instance,
        Investigation.Instance,
        Nature.Instance,
        Perception.Instance,
        Stealth.Instance,
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 3;

    private Ranger() { }

    public static readonly Ranger Instance = new Ranger();
}
