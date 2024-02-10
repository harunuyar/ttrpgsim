namespace DnD.Entities.Attributes;

internal class Attribute : IAttribute
{
    public Attribute(EAttributeType attributeType, int score)
    {
        AttributeType = attributeType;
        Score = score;
        SavingThrowProficiency = false;
    }

    public EAttributeType AttributeType { get; }

    public string Name => AttributeType.ToString();

    public int Score { get; set; }

    public bool SavingThrowProficiency { get; set; }
}
