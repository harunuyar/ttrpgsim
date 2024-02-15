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

public class Sorcerer : IClass
{
    public EDiceType HitDie => EDiceType.d6;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Constitution | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Sling | EWeaponType.Quarterstaff | EWeaponType.CrossbowLight;

    public string Name => "Sorcerer";

    public string Description => "A spellcaster who draws on inherent magic from a gift or bloodline";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Arcana.Instance,
        Deception.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    public EArmorType MulticlassArmorProficiencies => EArmorType.None;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.None;

    public int MulticlassNumberOfSkillProficiencies => 0;

    public EAttributeType SpellCastingAttribute => EAttributeType.Charisma;

    public EToolType ToolProficiencies => EToolType.None;

    public EToolType MulticlassToolProficiencies => EToolType.None;

    public Sorcerer() { }

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Charisma.Score >= 13;
    }
}
