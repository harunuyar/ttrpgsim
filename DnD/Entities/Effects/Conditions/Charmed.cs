namespace DnD.Entities.Effects.Conditions;

using DnD.Entities.Characters;

internal class Charmed : AEffect
{
    public Charmed(Character charmer)
        : base("Charmed", "A charmed creature can't attack the charmer or target the charmer with harmful abilities or magical effects. The charmer has advantage on any ability check to interact socially with the creature.")
    {
        Charmer = charmer;
    }

    public Character Charmer { get; set; }
}
