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

public class Warlock : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Warlock";

    public string Description => "A wielder of magic that is derived from a bargain with an extraplanar entity";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Arcana.Instance, 
        Deception.Instance, 
        History.Instance, 
        Intimidation.Instance, 
        Investigation.Instance, 
        Nature.Instance, 
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    public EArmorType MulticlassArmorProficiencies => EArmorType.Light;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.SimpleWeapon;

    public int MulticlassNumberOfSkillProficiencies => 0;

    public EAttributeType SpellCastingAttribute => EAttributeType.Charisma;

    public EToolType ToolProficiencies => EToolType.None;

    public EToolType MulticlassToolProficiencies => EToolType.None;

    public Warlock() { }

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Charisma.Score >= 13;
    }
}
