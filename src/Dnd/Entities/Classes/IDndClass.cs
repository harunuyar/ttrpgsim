namespace Dnd.Entities.Classes;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.GameManagers.Dice;

public interface IDndClass : IDndEntity
{
    string Description { get; }
    EDiceType HitDie { get; }
    EAttributeType SavingThrowProficiencies { get; }
    EArmorType ArmorProficiencies { get; }
    EWeaponType WeaponProficiencies { get; }
    List<IDndSkill> ChoosableSkillProficiencies { get; }
    int NumberOfSkillProficiencies { get; }
}
