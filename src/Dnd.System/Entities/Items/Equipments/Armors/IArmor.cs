namespace Dnd.System.Entities.Items.Equipments.Armors;

public interface IArmor : IItemDescription
{
    public EArmorType ArmorType { get; }

    public int BaseArmorClass { get; }
}
