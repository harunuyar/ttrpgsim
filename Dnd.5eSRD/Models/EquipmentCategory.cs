﻿namespace Dnd._5eSRD.Models.EquipmentCategory;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class EquipmentCategory : APIReference
{
    public List<APIReference>? Equipment { get; set; }
}