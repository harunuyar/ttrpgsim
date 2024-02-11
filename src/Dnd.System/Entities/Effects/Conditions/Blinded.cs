namespace Dnd.Entities.Effects.Conditions;

public class Blinded : AEffect
{
    public Blinded() 
        : base("Blinded", "A blinded creature can't see and automatically fails any ability check that requires sight. Attack rolls against the creature have advantage, and the creature's attack rolls have disadvantage.")
    {
    }
}
