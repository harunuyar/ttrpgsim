namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Invisible : AEffect
{
    public Invisible(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Invisible", "An invisible creature is impossible to see without the aid of magic or a special sense. For the purpose of hiding, the creature is heavily obscured. The creature's location can be detected by any noise it makes or any tracks it leaves. Attack rolls against the creature have disadvantage, and the creature's attack rolls have advantage.", 
            duration, source, target)
    { 
    }
}
