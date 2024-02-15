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

public class Bard : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Dexterity | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.CrossbowHand | EWeaponType.Longsword | EWeaponType.Rapier | EWeaponType.Shortsword;

    public string Name => "Bard";

    public string Description => "An inspiring magician whose power echoes the music of creation";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>
    {
        Acrobatics.Instance,
        AnimalHandling.Instance,
        Arcana.Instance,
        Athletics.Instance,
        Deception.Instance,
        History.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Nature.Instance,
        Perception.Instance,
        Performance.Instance,
        Persuasion.Instance,
        Religion.Instance,
        SleightOfHand.Instance,
        Stealth.Instance,
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 3;

    public EArmorType MulticlassArmorProficiencies => EArmorType.Light;

    public EWeaponType MulticlassWeaponProficiencies => EWeaponType.None;

    public int MulticlassNumberOfSkillProficiencies => 1;

    public EAttributeType SpellCastingAttribute => EAttributeType.Charisma;

    // three musical instruments of your choice
    public EToolType ToolProficiencies => EToolType.None;

    // one musical instrument of your choice
    public EToolType MulticlassToolProficiencies => EToolType.None;

    public Bard() { }

    public bool MeetsPrerequisites(AttributeSet attributeSet)
    {
        return attributeSet.Charisma.Score >= 13;
    }
}
