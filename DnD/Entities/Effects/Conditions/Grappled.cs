namespace DnD.Entities.Effects.Conditions;

internal class Grappled : IEffect
{
    public string Name => "Grappled";

    public string Description => "A grappled creature's speed becomes 0, and it can't benefit from any bonus to its speed.";
}
