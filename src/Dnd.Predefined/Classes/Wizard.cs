namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;
using Dnd.System.Entities.Characters;

public class Wizard : IClass
{
    public EDiceType HitDie => EDiceType.d6;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Intelligence | EAttributeType.Wisdom;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Sling | EWeaponType.Quarterstaff | EWeaponType.CrossbowLight;

    public string Name => "Wizard";

    public string Description => "A scholarly magic-user capable of manipulating the structures of reality";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Arcana.Instance,
        History.Instance,
        Insight.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    public EArmorType MulticlassArmorProficiencies => EArmorType.None;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.None;

    public int MulticlassNumberOfSkillProficiencies => 0;

    private Wizard() { }

    public static readonly Wizard Instance = new Wizard();

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Intelligence.Score >= 13;
    }
}
