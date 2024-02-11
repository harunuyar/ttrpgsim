namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;

public class Charmed : AEffect
{
    public Charmed(ICharacter charmer)
        : base("Charmed", "A charmed creature can't attack the charmer or target the charmer with harmful abilities or magical effects. The charmer has advantage on any ability check to interact socially with the creature.")
    {
        Charmer = charmer;
    }

    public ICharacter Charmer { get; set; }
}
