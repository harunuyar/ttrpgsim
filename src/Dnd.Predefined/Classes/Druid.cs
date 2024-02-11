namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;

public class Druid : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Intelligence | EAttributeType.Wisdom;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.Club | EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Javelin | EWeaponType.Mace | EWeaponType.Quarterstaff | EWeaponType.Scimitar | EWeaponType.Sickle | EWeaponType.Sling | EWeaponType.Spear;

    public string Name => "Druid";

    public string Description => "A priest of the Old Faith, wielding the powers of nature—moonlight and plant growth, fire and lightning—and adopting animal forms.";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>
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
