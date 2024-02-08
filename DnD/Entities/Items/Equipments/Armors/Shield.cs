﻿namespace DnD.Entities.Items.Equipments.Armors;

using DnD.Commands;
using DnD.Entities.Units;
using TableTopRpg.Commands;

internal class Shield : IItemDescription, ICommandInterrupter
{
    public Shield()
    {
        Name = "Shield";
        Description = "A shield is made from wood or metal and is carried in one hand. Wielding a shield increases your Armor Class by 2. You can benefit from only one shield at a time.";
        Weight = new Weight(6, EWeightUnit.Pounds);
        Value = new Worth(10, ECurrencyUnit.Gold);
        ArmorClass = 2;
    }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public string Name { get; set; }

    public string Description { get; set; }

    public Weight Weight { get; set; }

    public Worth Value { get; set; }

    public int ArmorClass { get; set; }

    public ICommandResult InterruptCommand(DndCommand command, ICommandResult currentResult)
    {
        if (command is GetArmorClassCommand && currentResult is IntegerResult integerResult && currentResult.IsSuccess)
        {
            integerResult.Value += ArmorClass;
        }

        return currentResult;
    }
}
