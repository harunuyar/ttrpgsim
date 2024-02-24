namespace Dnd._5eSRD.Models.Feat;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class Prerequisite
{
    public APIReference? AbilityScore { get; set; }
    public int? MinimumScore { get; set; }
}

public class FeatModel : APIReference
{
    public List<Prerequisite>? Prerequisites { get; set; }
    public List<string>? Desc { get; set; }
}