namespace DnD.Entities.Items;

using DnD.Entities.Units;

internal interface IItemDescription : IBonusProvider
{
    bool IsStackable { get; }
    bool IsConsumable { get; }
    bool IsEquippable { get; }
    string Description { get; }
    Weight Weight { get; }
    Value Value { get; }
}
