namespace Dnd.System.Entities.Items;

using Dnd.System.Entities.Units;

public interface IItemDescription : IBonusProvider
{
    bool IsStackable { get; }
    bool IsConsumable { get; }
    bool IsEquippable { get; }
    string Description { get; }
    Weight Weight { get; }
    Value Value { get; }
}
