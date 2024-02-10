namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;
using System.Collections.Generic;

public class Tiefling : IRace
{
    public static readonly Tiefling Instance = new Tiefling();

    private Tiefling()
    {
    }

    public string Name => "Tiefling";

    public string Description => "Tieflings are derived from human bloodlines, and in the broadest possible sense, they still look human";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
