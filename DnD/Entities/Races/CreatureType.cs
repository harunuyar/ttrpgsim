namespace DnD.Entities.Races;

using TableTopRpg.Entities.Character;

internal class CreatureType : ICreatureType
{
    public static readonly CreatureType Aberration = new CreatureType("Aberration", "Aberrations are utterly alien beings. Many of them have innate magical abilities drawn from the creature's alien mind rather than the mystical forces of the world.");
    public static readonly CreatureType Beast = new CreatureType("Beast", "Beasts are nonhumanoid creatures that are a natural part of the fantasy ecology. Some of them have magical powers, but most are unintelligent and lack any society or language.");
    public static readonly CreatureType Celestial = new CreatureType("Celestial", "Celestials are creatures native to the Upper Planes. Many of them are the servants of deities, employed as messengers, soldiers, spies, and assassins.");
    public static readonly CreatureType Construct = new CreatureType("Construct", "Constructs are made, not born. Some are programmed by their creators to follow a simple set of instructions, while others are fully sentient and capable of independent thought.");
    public static readonly CreatureType Dragon = new CreatureType("Dragon", "Dragons are large reptilian creatures of ancient origin and tremendous power. True dragons, including the good metallic dragons and the evil chromatic dragons, are highly intelligent and have innate magic.");
    public static readonly CreatureType Elemental = new CreatureType("Elemental", "Elementals are creatures native to the elemental planes.");
    public static readonly CreatureType Fey = new CreatureType("Fey", "Fey are magical creatures closely tied to the forces of nature. They dwell in twilight groves and misty forests.");
    public static readonly CreatureType Fiend = new CreatureType("Fiend", "Fiends are creatures of wickedness that are native to the Lower Planes. A few are the servants of deities, but many more labor under the leadership of archdevils and demon princes.");
    public static readonly CreatureType Giant = new CreatureType("Giant", "Giants tower over humans and their kind. They are humanlike in shape, though some have multiple heads (ettins) or deformities (fomorians).");
    public static readonly CreatureType Humanoid = new CreatureType("Humanoid", "Humanoids are the main peoples of a fantasy gaming world, both civilized and savage, including humans and a tremendous variety of other species.");
    public static readonly CreatureType Monstrosity = new CreatureType("Monstrosity", "Monstrosities are monsters in the strictest sense—frightening creatures that are not ordinary, not truly natural, and almost never benign.");
    public static readonly CreatureType Ooze = new CreatureType("Ooze", "Oozes are gelatinous creatures that rarely have a fixed shape. They are mostly subterranean, dwelling in caves and dungeons and feeding on refuse, carrion, or creatures unlucky enough to get in their way.");
    public static readonly CreatureType Plant = new CreatureType("Plant", "Plants in this context are vegetable creatures, not ordinary flora. Most of them are ambulatory, and some are carnivorous.");
    public static readonly CreatureType Undead = new CreatureType("Undead", "The undead are beings that have died and yet still wander the world. Many undead, such as vampires and liches, are evil and fiendish creatures.");

    private CreatureType(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }

    public string Description { get; }
}
