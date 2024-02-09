namespace DnD.Entities.Items.Equipments.Armors;

using DnD.CommandSystem.Commands;
using DnD.CommandSystem.Commands.IntegerResultCommands;
using DnD.Entities.Characters;
using DnD.Entities.Skills.Predefined;
using DnD.Entities.Units;

internal abstract class IArmor : IItemDescription, IBonusProvider
{
    public IArmor(EArmorType armorType, string name, string desc, int baseAC, Weight weight, Worth value, int strReq, bool disadvStealth)
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

    public string Name { get; set; }

    public string Description { get; set; }

    public Weight Weight { get; set; }

    public Worth Value { get; set; }

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
        if (command is MakeSkillCheckRoll makeSkillCheckRoll && DisadvantageToStealth && makeSkillCheckRoll.Skill == Stealth.Instance)
        {
            makeSkillCheckRoll.IntegerBonuses.AddAdvantage(this, EAdvantage.Disadvantage);
        }
    }
}
