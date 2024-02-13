namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CalculateWeaponAttackModifier : DndScoreCommand
{
    public CalculateWeaponAttackModifier(IGameActor character, IItem weaponItem, IGameActor target) : base(character)
    {
        this.WeaponItem = weaponItem;
        this.Target = target;
    }

    public IItem WeaponItem { get; }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
        if (WeaponItem.ItemDescription is not IWeapon weapon)
        {
            Result.SetError("Item is not a weapon");
            return;
        }

        if (weapon.SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
        {
            Result.SetError("Weapon doesn't use attack roll");
            return;
        }

        var getStrengthModifier = new GetAttributeModifier(this.Character, EAttributeType.Strength);
        var strengthModifier = getStrengthModifier.Execute();

        if (strengthModifier.IsSuccess)
        {
            IAttribute usedAttribute = this.Character.AttributeSet.GetAttribute(EAttributeType.Strength);
            int attributeModifier = strengthModifier.Value;

            if (weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
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

            var getWeaponProficiency = new HasWeaponProficiency(this.Character, weapon.WeaponType);
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
            return;
        }

        var calculateWeaponAttackModifierAgainstCharacter = new CalculateWeaponAttackModifier(this.Target, WeaponItem, Character);
        var attackModifierAgainstCharacter = calculateWeaponAttackModifierAgainstCharacter.Execute();

        if (attackModifierAgainstCharacter.IsSuccess)
        {
            if (attackModifierAgainstCharacter.BaseValue != 0)
            {
                Result.BonusCollection.AddBonus(attackModifierAgainstCharacter.BaseSource ?? new CustomDndEntity("Attack Modifier Base Against Character"), attackModifierAgainstCharacter.Value);
            }

            foreach (var bonus in attackModifierAgainstCharacter.BonusCollection.Values)
            {
                Result.BonusCollection.AddBonus(bonus.Key, bonus.Value);
            }
        }
    }

    protected override void FinalizeResult()
    {
    }
}
