namespace DnD.Entities.Effects.Conditions;

using DnD.Entities.Characters;

internal class Charmed : IEffect
{
    public Charmed(Character charmer)
    {
        Charmer = charmer;
    }

    public string Name => "Charmed";

    public string Description => "A charmed creature can't attack the charmer or target the charmer with harmful abilities or magical effects. The charmer has advantage on any ability check to interact socially with the creature.";

    public Character Charmer { get; set; }
}
