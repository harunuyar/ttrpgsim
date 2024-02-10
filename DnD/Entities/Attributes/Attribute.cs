namespace DnD.Entities.Attributes;

internal class Attribute : IAttribute
{
    public Attribute(EAttributeType attributeType, int score)
    {
        AttributeType = attributeType;
        Score = score;
        SavingThrowProficiencyLevel = 0;
    }

    public EAttributeType AttributeType { get; }

    public string Name => AttributeType.ToString();

    public int Score { get; set; }

    public int SavingThrowProficiencyLevel { get; set; }
}
