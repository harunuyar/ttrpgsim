namespace Dnd.Predefined.Items.Armors;

using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public abstract class AArmor : IArmor
{
    public AArmor(EArmorType armorType, string name, string desc, int baseAC, Weight weight, Value value, int strReq, bool disadvStealth)
    {
        ArmorType = armorType;
        Name = name;
        Description = desc;
        Weight = weight;
        Value = value;
        StrengthRequirement = strReq;
        DisadvantageToStealth = disadvStealth;
        BaseArmorClass = baseAC;
    }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public EArmorType ArmorType { get; set; }

    public string Name { get; }

    public string Description { get; set; }

    public Weight Weight { get; set; }

    public Value Value { get; set; }

    public int StrengthRequirement { get; set; }

    public bool DisadvantageToStealth { get; set; }

    public int BaseArmorClass { get; set; }

    public abstract int GetDexterityBonus(IGameActor character);

    protected static int GetDexterityModifier(IGameActor character)
    {
        var command = new GetAttributeModifier(character, EAttributeType.Dexterity);
        var result = command.Execute();
        return result.IsSuccess ? result.Value : 0;
    }

    public virtual void HandleCommand(ICommand command)
    {
        if (command is GetSkillModifier getSkillModifier && DisadvantageToStealth && getSkillModifier.Skill == Stealth.Instance)
        {
            getSkillModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
        else if (command is GetArmorClass getArmorClass)
        {
            getArmorClass.SetBaseValue(this, BaseArmorClass);

            int dexterityBonus = GetDexterityBonus(getArmorClass.Actor);
            int baseDexterityBonus = GetDexterityModifier(getArmorClass.Actor);

            int dexterityBonusDifference = dexterityBonus - baseDexterityBonus;
            if (dexterityBonusDifference != 0)
            {
                getArmorClass.AddBonus(this, dexterityBonusDifference);
            }
        }
        else if (command is CanEquipItem canEquipItem && canEquipItem.Item.ItemDescription == this)
        {
            var getStrengthScore = new GetAttributeScore(canEquipItem.Actor, EAttributeType.Strength);
            var strengthScore = getStrengthScore.Execute();

            if (strengthScore.IsSuccess && strengthScore.Value < StrengthRequirement)
            {
                canEquipItem.SetValue(this, false);
            }
        }
    }
}
