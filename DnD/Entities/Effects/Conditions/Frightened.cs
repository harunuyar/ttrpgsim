﻿namespace DnD.Entities.Effects.Conditions;

internal class Frightened : IEffect
{
    public string Name => "Frightened";

    public string Description => "A frightened creature has disadvantage on ability checks and attack rolls while the source of its fear is within line of sight.";
}
