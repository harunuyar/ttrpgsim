namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;
using System;

public class Aberration : ICreatureType
{
    public string Name => "Aberration";

    public string Description => "Aberrations are utterly alien beings. Many of them have innate magical abilities drawn from the creature's alien mind rather than the mystical forces of the world.";

    public static readonly Aberration Instance = new Aberration();

    private Aberration() { }
}
