namespace Dnd._5eSRD.Models.AbilityScore;

using Dnd._5eSRD.Models.Common;

public class AbilityScoreModel : APIReference
{
    public List<string>? Desc { get; set; }
    public string? FullName { get; set; }
    public List<APIReference>? Skills { get; set; }
}
