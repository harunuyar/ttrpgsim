namespace DnD.Entities.Items.Equipments.Armors;

using DnD.CommandSystem.Commands;
using DnD.CommandSystem.Commands.IntegerResultCommands;
using DnD.Entities.Units;

internal class Shield : IItemDescription
{
    public Shield()
    {
        Name = "Shield";
        Description = "A shield is made from wood or metal and is carried in one hand. Wielding a shield increases your Armor Class by 2. You can benefit from only one shield at a time.";
        Weight = new Weight(6, EWeightUnit.Pounds);
        Value = new Value(10, ECurrencyUnit.Gold);
        ArmorClass = 2;
    }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public string Name { get; }

    public string Description { get; set; }

    public Weight Weight { get; set; }

    public Value Value { get; set; }

    public int ArmorClass { get; set; }

    public virtual void HandleCommand(DndCommand command)
    {
        if (command is GetArmorClass getArmorClass)
        {
            getArmorClass.Result.BonusCollection.AddBonus(this, ArmorClass);
        }
    }
}
