namespace DnD.Entities.Items.Equipments.Armors;

using DnD.Commands;
using DnD.Entities.Characters;
using DnD.Entities.Skills.Predefined;
using DnD.Entities.Units;
using TableTopRpg.Commands;

internal abstract class IArmor : IItemDescription, ICommandInterrupter
{
    public IArmor(EArmorType armorType, string name, string desc, Weight weight, Worth value, int strReq, bool disadvStealth)
    {
        ArmorType = armorType;
        Name = name;
        Description = desc;
        Weight = weight;
        Value = value;
        StrengthRequirement = strReq;
        DisadvantageToStealth = disadvStealth;
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

    public abstract int GetArmorClass(Character character);

    public ICommandResult InterruptCommand(DndCommand command, ICommandResult currentResult)
    {
        if (command is CalculateSkillAdvantageCommand calculateSkillAdvantageCommand)
        {
            if (calculateSkillAdvantageCommand.Skill == Stealth.Instance && DisadvantageToStealth)
            {
                int value = currentResult is IntegerResult integerResult ? integerResult.Value : 0;
                return IntegerResult.Success(calculateSkillAdvantageCommand, value & (int)EAdvantage.Disadvantage);
            }
        }

        return currentResult;
    }

    protected static int GetDexterityModifier(Character character)
    {
        ICommand command = new GetAttributeModifierCommand(character, Attributes.EAttributeType.Dexterity);
        ICommandResult result = command.Execute();

        if (result.IsSuccess && result is IntegerResult integerResult)
        {
            return integerResult.Value;
        }

        return 0;
    }
}
