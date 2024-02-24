namespace Dnd._5eSRD.Models.Language;

using Dnd._5eSRD.Models.Common;
using System.Collections.Generic;

public class LanguageModel : APIReference
{
    public string? Desc { get; set; }
    public string? Script { get; set; }
    public string? Type { get; set; }
    public List<string>? TypicalSpeakers { get; set; }
}