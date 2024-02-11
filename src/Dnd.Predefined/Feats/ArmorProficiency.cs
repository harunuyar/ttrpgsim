namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Items.Equipments.Armors;

public class ArmorProficiency : AFeat
{
    public ArmorProficiency(EArmorType armorType) : base("Armor Proficiency", GetDescription(armorType))
    {
        ArmorType = armorType;
    }

    public EArmorType ArmorType { get; }

    public override void HandleCommand(DndCommand command)
    {
        if (command is HasArmorProficiency hasArmorProficiency && ArmorType.HasFlag(hasArmorProficiency.ArmorType))
        {
            hasArmorProficiency.Result.SetValue(this, true);
        }
    }

    private static string GetDescription(EArmorType armorType)
    {
        var list = new List<string>();

        if (armorType.HasFlag(EArmorType.All))
        {
            list.Add("All");
        }
        else
        {
            if (armorType.HasFlag(EArmorType.Light))
            {
                list.Add("Light");
            }

            if (armorType.HasFlag(EArmorType.Medium))
            {
                list.Add("Medium");
            }

            if (armorType.HasFlag(EArmorType.Heavy))
            {
                list.Add("Heavy");
            }
            
            if (armorType.HasFlag(EArmorType.Shield))
            {
                list.Add("Shield");
            }
        }

        return "You gain proficiency with the following armor types: " + string.Join(", ", list);
    }
}
