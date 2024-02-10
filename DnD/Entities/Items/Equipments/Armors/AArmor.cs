namespace DnD.Entities.Items.Equipments.Armors;

using DnD.CommandSystem.Commands;
using DnD.CommandSystem.Commands.IntegerResultCommands;
using DnD.Entities.Advantage;
using DnD.Entities.Characters;
using DnD.Entities.Skills.Predefined;
using DnD.Entities.Units;

internal abstract class AArmor : IItemDescription
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

    public abstract int GetDexterityBonus(Character character);

    protected static int GetDexterityModifier(Character character)
    {
        var command = new GetAttributeModifier(character, Attributes.EAttributeType.Dexterity);
        var result = command.Execute();
        return result.IsSuccess ? result.Value : 0;
    }

    public virtual void HandleCommand(DndCommand command)
    {
        if (command is GetSkillModifier getSkillModifier && DisadvantageToStealth && getSkillModifier.Skill == Stealth.Instance)
        {
            getSkillModifier.Result.BonusCollection.AddAdvantage(this, EAdvantage.Disadvantage);
        }
        else if (command is GetArmorClass getArmorClass)
        {
            getArmorClass.Result.SetBaseValue(this, BaseArmorClass);

            int dexterityBonus = GetDexterityBonus(getArmorClass.Character);
            int baseDexterityBonus = getArmorClass.Result.BonusCollection.GetBonus(getArmorClass.Character.AttributeSet.Dexterity);

            int dexterityBonusDifference = dexterityBonus - baseDexterityBonus;
            if (dexterityBonusDifference != 0)
            {
                getArmorClass.Result.BonusCollection.AddBonus(this, dexterityBonusDifference);
            }
        }
    }
}
