namespace Dnd._5eSRD.Models.Feature;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class Prerequisite
{
    public PrerequisiteType? Type { get; set; }

    // level
    public int? Level { get; set; }

    // feature
    public string? Feature { get; set; }

    // spell
    public string? Spell { get; set; }
}

public enum PrerequisiteType
{
    Level,
    Feature,
    Spell
}

public class FeatureSpecificModel
{
    public Choice? SubfeatureOptions { get; set; }
    public Choice? ExpertiseOptions { get; set; }
    public List<APIReference>? Invocations { get; set; }
}

public class FeatureModel : APIReference
{
    public APIReference? Class { get; set; }
    public List<string>? Desc { get; set; }
    public APIReference? Parent { get; set; }
    public int? Level { get; set; }
    public List<Prerequisite>? Prerequisites { get; set; }
    public string? Reference { get; set; }
    public APIReference? Subclass { get; set; }
    public FeatureSpecificModel? FeatureSpecific { get; set; }
}