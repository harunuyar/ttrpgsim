namespace Dnd._5eSRD.Models.MagicItem;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class Rarity
{
    public string? Name { get; set; }
}

public class MagicItem : APIReference
{
    public List<string>? Desc { get; set; }
    public APIReference? EquipmentCategory { get; set; }
    public Rarity? Rarity { get; set; }
    public List<APIReference>? Variants { get; set; }
    public bool? Variant { get; set; }
}