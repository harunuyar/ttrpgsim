namespace Dnd._5eSRD.Models.Rule;

using Dnd._5eSRD.Models.Common;

public class Rule : APIReference
{
    public string? Desc { get; set; }
    public List<APIReference>? Subsections { get; set; }
}