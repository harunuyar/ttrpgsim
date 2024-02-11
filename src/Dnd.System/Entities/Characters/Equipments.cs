﻿namespace Dnd.Entities.Characters;

using Dnd.Entities.Items;

public class Equipments
{
    public Equipments()
    {
        this.EquipedItems = new HashSet<IItem>();
    }

    public IItem? Armor { get; set; }

    public IItem? Shield { get; set; }

    public IItem? MainHandWeapon { get; set; }

    public IItem? OffHandWeapon { get; set; }

    public HashSet<IItem> EquipedItems { get; }
}