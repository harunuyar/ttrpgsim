namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;
using Dnd.System.Entities.GameActors;

public class Ranger : IClass
{
    public EDiceType HitDie => EDiceType.d10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Dexterity;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Ranger";

    public string Description => "A warrior who combats threats on the edges of civilization";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
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

    public EArmorType MulticlassArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.All;

    public int MulticlassNumberOfSkillProficiencies => 1;

    public EAttributeType SpellCastingAttribute => EAttributeType.Wisdom;

    private Ranger() { }

    public static readonly Ranger Instance = new Ranger();

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Dexterity.Score >= 13 && attributeSet.Wisdom.Score >= 13;
    }
}
