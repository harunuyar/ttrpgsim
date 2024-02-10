using Dnd.Entities.Attributes;

namespace Dnd.Entities.Characters;

public class AttributeSet
{
    public AttributeSet()
    {
        Strength = new Attributes.Attribute(EAttributeType.Strength, 10);
        Dexterity = new Attributes.Attribute(EAttributeType.Dexterity, 10);
        Constitution = new Attributes.Attribute(EAttributeType.Constitution, 10);
        Intelligence = new Attributes.Attribute(EAttributeType.Intelligence, 10);
        Wisdom = new Attributes.Attribute(EAttributeType.Wisdom, 10);
        Charisma = new Attributes.Attribute(EAttributeType.Charisma, 10);
    }

    public AttributeSet(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        Strength = new Attributes.Attribute(EAttributeType.Strength, strength);
        Dexterity = new Attributes.Attribute(EAttributeType.Dexterity, dexterity);
        Constitution = new Attributes.Attribute(EAttributeType.Constitution, constitution);
        Intelligence = new Attributes.Attribute(EAttributeType.Intelligence, intelligence);
        Wisdom = new Attributes.Attribute(EAttributeType.Wisdom, wisdom);
        Charisma = new Attributes.Attribute(EAttributeType.Charisma, charisma);
    }

    public Attributes.Attribute Strength { get; set; }
    public Attributes.Attribute Dexterity { get; set; }
    public Attributes.Attribute Constitution { get; set; }
    public Attributes.Attribute Intelligence { get; set; }
    public Attributes.Attribute Wisdom { get; set; }
    public Attributes.Attribute Charisma { get; set; }

    public void Set(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        Strength.Score = strength;
        Dexterity.Score = dexterity;
        Constitution.Score = constitution;
        Intelligence.Score = intelligence;
        Wisdom.Score = wisdom;
        Charisma.Score = charisma;
    }

    public Attributes.Attribute GetAttribute(EAttributeType attributeType)
    {
        return attributeType switch
        {
            EAttributeType.Strength => Strength,
            EAttributeType.Dexterity => Dexterity,
            EAttributeType.Constitution => Constitution,
            EAttributeType.Intelligence => Intelligence,
            EAttributeType.Wisdom => Wisdom,
            EAttributeType.Charisma => Charisma,
            _ => throw new ArgumentOutOfRangeException(nameof(attributeType), attributeType, "Unknown attribute type")
        };
    }
}
