namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.Entities;
using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class CalculateWeaponAttackModifier : DndScoreCommand
{
    public CalculateWeaponAttackModifier(Character character, AWeapon weapon) : base(character)
    {
        this.Weapon = weapon;
    }

    public AWeapon Weapon { get; }

    public override void InitializeResult()
    {
        if (Weapon.SuccessMeasuringType == ESuccessMeasuringType.AttackRoll)
        {
            var getStrengthModifier = new GetAttributeModifier(this.Character, EAttributeType.Strength);
            var strengthModifier = getStrengthModifier.Execute();

            if (strengthModifier.IsSuccess)
            {
                IAttribute usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Strength);
                int attributeModifier = strengthModifier.Value;

                if (Weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
                {
                    var getDexterityModifier = new GetAttributeModifier(this.Character, EAttributeType.Dexterity);
                    var dexterityModifier = getDexterityModifier.Execute();

                    if (dexterityModifier.IsSuccess && dexterityModifier.Value > attributeModifier)
                    {
                        attributeModifier = dexterityModifier.Value;
                        usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Dexterity);
                    }
                }

                Result.SetBaseValue(usedAttribute, attributeModifier);

                var getWeaponProficiency = new HasWeaponProficiency(this.Character, this.Weapon.WeaponType);
                var weaponProficiency = getWeaponProficiency.Execute();

                if (weaponProficiency.IsSuccess && weaponProficiency.Value)
                {
                    var getProficiencyBonus = new GetProficiencyBonus(this.Character);
                    var proficiencyBonus = getProficiencyBonus.Execute();

                    if (proficiencyBonus.IsSuccess)
                    {
                        Result.BonusCollection.AddBonus("Proficiency Bonus", proficiencyBonus.Value);
                    }
                }
            }
            else
            {
                Result.SetError(strengthModifier.ErrorMessage ?? "Couldn't get attribute modifier");
            }
        }
        else
        {
            Result.SetError("Weapon doesn't use attack roll");
        }
    }
}
