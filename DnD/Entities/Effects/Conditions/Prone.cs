namespace DnD.Entities.Effects.Conditions;

internal class Prone : IEffect
{
    public string Name => "Prone";

    public string Description => "A prone creature's only movement option is to crawl, unless it stands up and thereby ends the condition.";
}
