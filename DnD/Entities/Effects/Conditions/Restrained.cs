namespace DnD.Entities.Effects.Conditions;

internal class Restrained : IEffect
{
    public string Name => "Restrained";

    public string Description => "A restrained creature's speed becomes 0, and it can't benefit from any bonus to its speed.";
}
