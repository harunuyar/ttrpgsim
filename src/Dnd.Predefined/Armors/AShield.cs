namespace Dnd.Predefined.Armors;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public abstract class AShield : IShield
{
    public AShield(string name, string description, Weight weight, Value value, int armorClass)
    {
        Name = name;
        Description = description;
        Weight = weight;
        Value = value;
        ArmorClass = armorClass;
    }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public string Name { get; }

    public string Description { get; }

    public Weight Weight { get; }

    public Value Value { get; }

    public int ArmorClass { get; }

    public virtual void HandleCommand(ICommand command)
    {
        if (command is GetArmorClass getArmorClass)
        {
            getArmorClass.AddBonus(this, ArmorClass);
        }
    }
}
