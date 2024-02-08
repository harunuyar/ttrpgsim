namespace DnD.Entities.Items;

using DnD.Entities.Units;

internal interface IItemDescription
{
    bool IsStackable { get; }
    bool IsConsumable { get; }
    bool IsEquippable { get; }
    string Name { get; }
    string Description { get; }
    Weight Weight { get; }
    Worth Value { get; }
}
