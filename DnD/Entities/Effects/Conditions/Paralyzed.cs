﻿namespace DnD.Entities.Effects.Conditions;

internal class Paralyzed : IEffect
{
    public string Name => "Paralyzed";

    public string Description => "A paralyzed creature is incapacitated and can't move or speak. The creature automatically fails Strength and Dexterity saving throws. Attack rolls against the creature have advantage. Any attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
}