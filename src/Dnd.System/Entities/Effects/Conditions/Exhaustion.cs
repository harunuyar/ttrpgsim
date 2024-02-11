namespace Dnd.Entities.Effects.Conditions;

public class Exhaustion : AEffect
{
    public Exhaustion(int level = 1)
        : base("Exhaustion", "Exhaustion is measured in six levels. An effect can give a creature one or more levels of exhaustion, as specified in the effect's description.")
    {
        Level = level;
    }

    public int Level { get; set; }
}