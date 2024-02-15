namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Tools;

public class Rogue : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Dexterity | EAttributeType.Intelligence;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.CrossbowHand | EWeaponType.Longsword | EWeaponType.Rapier | EWeaponType.Shortsword;

    public string Name => "Rogue";

    public string Description => "A scoundrel who uses stealth and trickery to overcome obstacles and enemies";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Acrobatics.Instance,
        Athletics.Instance,
        Deception.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Investigation.Instance,
        Perception.Instance,
        Performance.Instance,
        Persuasion.Instance,
        SleightOfHand.Instance,
        Stealth.Instance
    };

    public int NumberOfSkillProficiencies => 4;

    public EArmorType MulticlassArmorProficiencies => EArmorType.Light;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.None;

    public int MulticlassNumberOfSkillProficiencies => 1;

    public EAttributeType SpellCastingAttribute => EAttributeType.None;

    public EToolType ToolProficiencies => EToolType.Thieves;

    public EToolType MulticlassToolProficiencies => EToolType.Thieves;

    public Rogue() { }

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Dexterity.Score >= 13;
    }
}
