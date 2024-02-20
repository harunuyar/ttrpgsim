namespace Dnd._5eSRD.Models.Equipment;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class ArmorClass
{
    public int? Base { get; set; }
    public bool? DexBonus { get; set; }
    public int? MaxBonus { get; set; }
}

public class Content
{
    public APIReference? Item { get; set; }
    public int? Quantity { get; set; }
}

public class Cost
{
    public int? Quantity { get; set; }
    public string? Unit { get; set; }
}

public class Range
{
    public int? Long { get; set; }
    public int Normal { get; set; }
}

public class Speed
{
    public double? Quantity { get; set; }
    public string? Unit { get; set; }
}

public class ThrowRange
{
    public int? Long { get; set; }
    public int? Normal { get; set; }
}

public class TwoHandedDamage
{
    public string? DamageDice { get; set; }
    public APIReference? DamageType { get; set; }
}

public class Equipment : APIReference
{
    public string? ArmorCategory { get; set; }
    public ArmorClass? ArmorClass { get; set; }
    public string? Capacity { get; set; }
    public string? CategoryRange { get; set; }
    public List<Content>? Contents { get; set; }
    public Cost? Cost { get; set; }
    public Damage? Damage { get; set; }
    public List<string>? Desc { get; set; }
    public APIReference? EquipmentCategory { get; set; }
    public APIReference? GearCategory { get; set; }
    public List<APIReference>? Properties { get; set; }
    public int? Quantity { get; set; }
    public Range? Range { get; set; }
    public List<string>? Special { get; set; }
    public Speed? Speed { get; set; }
    public bool? StealthDisadvantage { get; set; }
    public int? StrMinimum { get; set; }
    public ThrowRange? ThrowRange { get; set; }
    public string? ToolCategory { get; set; }
    public TwoHandedDamage? TwoHandedDamage { get; set; }
    public string? VehicleCategory { get; set; }
    public string? WeaponCategory { get; set; }
    public string? WeaponRange { get; set; }
    public double? Weight { get; set; }
}