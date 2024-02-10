namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;
using Dnd.Entities.Traits.Dragonborn;
using System.Collections.Generic;

public class Dragonborn : IRace
{
    public static readonly Dragonborn Instance = new Dragonborn();

    private Dragonborn()
    {
    }

    public string Name => "Dragonborn";

    public string Description => "Dragonborn look very much like dragons standing erect in humanoid form, though they lack wings or a tail.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>() { AbilityScoreIncrease.Instance };
}
