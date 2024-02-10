namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Commands.BooleanResultCommands;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Weapons;

internal class CalculateAttackModifier : DndScoreCommand
{
    public CalculateAttackModifier(Character character, AWeapon weapon) : base(character)
    {
        this.Weapon = weapon;
    }

    public AWeapon Weapon { get; }

    public override void InitializeResult()
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

            var getWeaponProficiency = new GetWeaponProficiency(this.Character, this.Weapon.WeaponType);
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
}
