namespace Dnd.Entities.Spells;

public class AAttackingSpell : ISpell
{
    public AAttackingSpell(string name, string description, ESuccessMeasuringType successMeasuringType)
    {
        Name = name;
        Description = description;
        SuccessMeasuringType = successMeasuringType;
    }

    public string Name { get; }

    public string Description { get; }

    public ESuccessMeasuringType SuccessMeasuringType { get; }
}
