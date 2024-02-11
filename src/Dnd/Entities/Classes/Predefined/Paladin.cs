namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Paladin : IDndClass
{
    public EDiceType HitDie => EDiceType.d10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.All;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Paladin";

    public string Description => "A holy warrior bound to a sacred oath";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Athletics.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Medicine.Instance,
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Paladin() { }

    public static readonly Paladin Instance = new Paladin();
}
