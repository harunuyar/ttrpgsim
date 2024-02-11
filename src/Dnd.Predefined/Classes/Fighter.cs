namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;

public class Fighter : IClass
{
    public EDiceType HitDie => EDiceType.d10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Constitution;

    public EArmorType ArmorProficiencies => EArmorType.All;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Fighter";

    public string Description => "A master of martial combat, skilled with a variety of weapons and armor";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Acrobatics.Instance,
        AnimalHandling.Instance,
        Athletics.Instance,
        History.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Perception.Instance,
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Fighter() { }

    public static readonly Fighter Instance = new Fighter();
}
