namespace Dnd.System.Entities.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;

public interface IClass : IDndEntity
{
    string Description { get; }

    EDiceType HitDie { get; }

    EAttributeType SavingThrowProficiencies { get; }

    EArmorType ArmorProficiencies { get; }

    EWeaponType WeaponProficiencies { get; }

    List<ISkill> ChoosableSkillProficiencies { get; }

    int NumberOfSkillProficiencies { get; }
}
