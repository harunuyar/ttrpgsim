namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.Predefined.Skills;
using Dnd.System.Entities.Classes;

public class Artificer : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Constitution | EAttributeType.Intelligence;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Artificer";

    public string Description => "A master of unlocking magic in everyday objects, artificers are supreme inventors. They see magic as a complex system waiting to be decoded and then harnessed in their spells and inventions.";

    public List<ISkill> ChoosableSkillProficiencies => new()
    {
        Arcana.Instance,
        History.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Nature.Instance,
        Perception.Instance,
        SleightOfHand.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Artificer() { }

    public static readonly Artificer Instance = new Artificer();
}
