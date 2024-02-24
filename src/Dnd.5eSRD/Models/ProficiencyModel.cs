namespace Dnd._5eSRD.Models.Proficiency;

using Dnd._5eSRD.Models.Common;

public class Reference
{
    public string? Index { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Url { get; set; }
}

public class ProficiencyModel : APIReference
{
    public List<APIReference>? Classes { get; set; }
    public List<APIReference>? Races { get; set; }
    public Reference? Reference { get; set; }
    public string? Type { get; set; }
}
