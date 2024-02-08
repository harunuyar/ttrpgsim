namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Druid : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Intelligence | EAttributeType.Wisdom;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.Club | EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Javelin | EWeaponType.Mace | EWeaponType.Quarterstaff | EWeaponType.Scimitar | EWeaponType.Sickle | EWeaponType.Sling | EWeaponType.Spear;

    public string Name => "Druid";

    public string Description => "A priest of the Old Faith, wielding the powers of nature—moonlight and plant growth, fire and lightning—and adopting animal forms.";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>
    {
        Arcana.Instance, 
        AnimalHandling.Instance, 
        Insight.Instance, 
        Medicine.Instance, 
        Nature.Instance, 
        Perception.Instance, 
        Religion.Instance, 
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Druid() { }

    public static readonly Druid Instance = new Druid();
}
