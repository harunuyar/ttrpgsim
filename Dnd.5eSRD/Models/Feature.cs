namespace Dnd._5eSRD.Models.Feature;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class Prerequisite
{
    public string? Type { get; set; }
    public int? Level { get; set; }
    public string? Feature { get; set; }
    public string? Spell { get; set; }
}

public class LevelPrerequisite
{
    public string? Type { get; set; }
    public int? Level { get; set; }
}

public class FeaturePrerequisite
{
    public string? Type { get; set; }
    public string? Feature { get; set; }
}

public class SpellPrerequisite
{
    public string? Type { get; set; }
    public string? Spell { get; set; }
}

public class FeatureSpecific
{
    public Choice? SubfeatureOptions { get; set; }
    public Choice? ExpertiseOptions { get; set; }
    public List<APIReference>? Invocations { get; set; }
}

public class Feature : APIReference
{
    public APIReference? Class { get; set; }
    public List<string>? Desc { get; set; }
    public APIReference? Parent { get; set; }
    public int? Level { get; set; }
    public List<Prerequisite>? Prerequisites { get; set; }
    public string? Reference { get; set; }
    public APIReference? Subclass { get; set; }
    public FeatureSpecific? FeatureSpecific { get; set; }
}