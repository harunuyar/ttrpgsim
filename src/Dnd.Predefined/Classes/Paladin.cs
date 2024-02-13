namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;
using Dnd.System.Entities.GameActors;

public class Paladin : IClass
{
    public EDiceType HitDie => EDiceType.d10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.All;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Paladin";

    public string Description => "A holy warrior bound to a sacred oath";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Athletics.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Medicine.Instance,
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    public EArmorType MulticlassArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.All;

    public int MulticlassNumberOfSkillProficiencies => 0;

    public EAttributeType SpellCastingAttribute => EAttributeType.Charisma;

    private Paladin() { }

    public static readonly Paladin Instance = new Paladin();

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Strength.Score >= 13 && attributeSet.Charisma.Score >= 13;
    }
}
