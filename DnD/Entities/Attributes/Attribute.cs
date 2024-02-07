namespace DnD.Entities.Attributes;

using TableTopRpg.Entities.Character;

internal class Attribute : IAttribute
{
    public Attribute(EAttributeType attributeType, int value)
    {
        AttributeType = attributeType;
        Value = value;
        SavingThrowProficiencyLevel = 0;
    }

    public EAttributeType AttributeType { get; }

    public string Name => AttributeType.ToString();

    public int Value { get; set; }

    public int SavingThrowProficiencyLevel { get; set; }

    public int GetModifier()
    {
        return (Value - 10) / 2;
    }

    public int GetSavingThrowModifier()
    {
        return GetModifier() + SavingThrowProficiencyLevel * 2;
    }
}
