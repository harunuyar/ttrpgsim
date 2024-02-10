namespace DnD.Entities.Races;

using DnD.Entities.Traits;
using System.Collections.Generic;

internal class Race : IRace
{
    public static readonly Race Dragonborn = new Race("Dragonborn", "Dragonborn", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Dwarf = new Race("Dwarf", "Dwarf", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Elf = new Race("Elf", "Elf", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Gnome = new Race("Gnome", "Gnome", Races.CreatureType.Humanoid, Races.Size.Small);
    public static readonly Race HalfElf = new Race("HalfElf", "Half-Elf", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race HalfOrc = new Race("HalfOrc", "Half-Orc", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Halfling = new Race("Halfling", "Halfling", Races.CreatureType.Humanoid, Races.Size.Small);
    public static readonly Race Human = new Race("Human", "Human", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Tiefling = new Race("Tiefling", "Tiefling", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Aarakocra = new Race("Aarakocra", "Aarakocra", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Aasimar = new Race("Aasimar", "Aasimar", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race AirGenasi = new Race("AirGenasi", "Air Genasi", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Bugbear = new Race("Bugbear", "Bugbear", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Centaur = new Race("Centaur", "Centaur", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Changeling = new Race("Changeling", "Changeling", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race DeepGnome = new Race("DeepGnome", "Deep Gnome", Races.CreatureType.Humanoid, Races.Size.Small);
    public static readonly Race Duergar = new Race("Duergar", "Duergar", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race EarthGenasi = new Race("EarthGenasi", "Earth Genasi", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Eladrin = new Race("Eladrin", "Eladrin", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Fairy = new Race("Fairy", "Fairy", Races.CreatureType.Humanoid, Races.Size.Small);
    public static readonly Race Firbolg = new Race("Firbolg", "Firbolg", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race FireGenasi = new Race("FireGenasi", "Fire Genasi", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Githyanki = new Race("Githyanki", "Githyanki", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Githzerai = new Race("Githzerai", "Githzerai", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Goblin = new Race("Goblin", "Goblin", Races.CreatureType.Humanoid, Races.Size.Small);
    public static readonly Race Goliath = new Race("Goliath", "Goliath", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Harengon = new Race("Harengon", "Harengon", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Hobgoblin = new Race("Hobgoblin", "Hobgoblin", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Kenku = new Race("Kenku", "Kenku", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Kobold = new Race("Kobold", "Kobold", Races.CreatureType.Humanoid, Races.Size.Small);
    public static readonly Race Lizardfolk = new Race("Lizardfolk", "Lizardfolk", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Minotaur = new Race("Minotaur", "Minotaur", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Orc = new Race("Orc", "Orc", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Satyr = new Race("Satyr", "Satyr", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race SeaElf = new Race("SeaElf", "Sea Elf", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race ShadarKai = new Race("ShadarKai", "Shadar-Kai", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Shifter = new Race("Shifter", "Shifter", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Tabaxi = new Race("Tabaxi", "Tabaxi", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Tortle = new Race("Tortle", "Tortle", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race Triton = new Race("Triton", "Triton", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race WaterGenasi = new Race("WaterGenasi", "Water Genasi", Races.CreatureType.Humanoid, Races.Size.Medium);
    public static readonly Race YuanTi = new Race("YuanTi", "Yuan-Ti", Races.CreatureType.Humanoid, Races.Size.Medium);

    public Race(string name, string description, ICreatureType creatureType, ISize size)
    {
        Name = name;
        Description = description;
        CreatureType = creatureType;
        Size = size;
        RaceTraits = new List<ATrait>();
    }

    public string Name { get; }

    public string Description { get; }

    public ICreatureType CreatureType { get; }

    public ISize Size { get; }

    public List<ATrait> RaceTraits { get; }
}
