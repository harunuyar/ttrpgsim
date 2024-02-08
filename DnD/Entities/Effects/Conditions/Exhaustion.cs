namespace DnD.Entities.Effects.Conditions;

internal class Exhaustion : IEffect
{
    public Exhaustion(int level)
    {
        Level = level;
    }

    public string Name => "Exhaustion";

    public string Description => "Exhaustion is measured in six levels. An effect can give a creature one or more levels of exhaustion, as specified in the effect's description.";

    public int Level { get; set; }
}