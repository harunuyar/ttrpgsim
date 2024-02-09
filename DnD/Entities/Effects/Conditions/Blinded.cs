﻿namespace DnD.Entities.Effects.Conditions;

using DnD.CommandSystem;
using TableTopRpg.Commands;

internal class Blinded : IEffect
{
    public string Name => "Blinded";

    public string Description => "A blinded creature can't see and automatically fails any ability check that requires sight. Attack rolls against the creature have advantage, and the creature's attack rolls have disadvantage.";
}
