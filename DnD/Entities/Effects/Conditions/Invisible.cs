﻿namespace DnD.Entities.Effects.Conditions;

internal class Invisible : IEffect
{
    public string Name => "Invisible";

    public string Description => "An invisible creature is impossible to see without the aid of magic or a special sense. For the purpose of hiding, the creature is heavily obscured. The creature's location can be detected by any noise it makes or any tracks it leaves. Attack rolls against the creature have disadvantage, and the creature's attack rolls have advantage.";
}
