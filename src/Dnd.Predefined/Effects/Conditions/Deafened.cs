﻿namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Deafened : AEffect
{
    public Deafened(IEffectDuration duration, ICharacter source, ICharacter target) 
        : base("Deafened", "A deafened creature can't hear and automatically fails any ability check that requires hearing.", duration, source, target)
    {
    }
}
