namespace DnD.Entities.Effects.Conditions;

internal class Deafened : IEffect
{
    public string Name => "Deafened";

    public string Description => "A deafened creature can't hear and automatically fails any ability check that requires hearing.";
}
