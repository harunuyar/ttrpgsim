namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;
using Dnd.System.Entities.GameActors;

public class Cleric : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Cleric";

    public string Description => "A priestly champion who wields divine magic in service of a higher power.";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill> 
    { 
        History.Instance, 
        Insight.Instance, 
        Medicine.Instance, 
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    public EArmorType MulticlassArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.None;

    public int MulticlassNumberOfSkillProficiencies => 0;

    public EAttributeType SpellCastingAttribute => EAttributeType.Wisdom;

    private Cleric() { }

    public static readonly Cleric Instance = new Cleric();

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Wisdom.Score >= 13;
    }
}
