namespace Dnd.Entities.Items;

using Dnd.Entities.Units;

public interface IItemDescription : IBonusProvider
{
    bool IsIdentified { get; }
    bool IsStackable { get; }
    bool IsConsumable { get; }
    bool IsEquippable { get; }
    string Description { get; }
    Weight Weight { get; }
    Value Value { get; }
}
