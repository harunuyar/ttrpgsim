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
}
