namespace DnD.Entities.Classes;

using DnD.GameManagers.Dice;

internal class Class : IClass
{
    public static readonly IClass None = new Class("None", "No class", EDiceType.D4);
    public static readonly IClass Barbarian = new Class("Barbarian", "A fierce warrior of primitive background who can enter a battle rage", EDiceType.D12);
    public static readonly IClass Bard = new Class("Bard", "An inspiring magician whose power echoes the music of creation", EDiceType.D8);
    public static readonly IClass Cleric = new Class("Cleric", "A priestly champion who wields divine magic in service of a higher power", EDiceType.D8);
    public static readonly IClass Druid = new Class("Druid", "A priest of the Old Faith, wielding the powers of nature—moonlight and plant growth, fire and lightning—and adopting animal forms", EDiceType.D8);
    public static readonly IClass Fighter = new Class("Fighter", "A master of martial combat, skilled with a variety of weapons and armor", EDiceType.D10);
    public static readonly IClass Monk = new Class("Monk", "A master of martial arts, harnessing the power of the body in pursuit of physical and spiritual perfection", EDiceType.D8);
    public static readonly IClass Paladin = new Class("Paladin", "A holy warrior bound to a sacred oath", EDiceType.D10);
    public static readonly IClass Ranger = new Class("Ranger", "A warrior who uses martial prowess and nature magic to combat threats on the edges of civilization", EDiceType.D10);
    public static readonly IClass Rogue = new Class("Rogue", "A scoundrel who uses stealth and trickery to overcome obstacles and enemies", EDiceType.D8);
    public static readonly IClass Sorcerer = new Class("Sorcerer", "A spellcaster who draws on inherent magic from a gift or bloodline", EDiceType.D6);
    public static readonly IClass Warlock = new Class("Warlock", "A wielder of magic that is derived from a bargain with an extraplanar entity", EDiceType.D8);
    public static readonly IClass Wizard = new Class("Wizard", "A scholarly magic-user capable of manipulating the structures of reality", EDiceType.D6);
    public static readonly IClass Artificer = new Class("Artificer", "A master of unlocking magic in everyday objects, using it to invent and create", EDiceType.D8);
    public static readonly IClass BloodHunter = new Class("BloodHunter", "A fanatical slayer that embraces dark knowledge to destroy evil", EDiceType.D10);

    private Class(string name, string description, EDiceType hitDie)
    {
        Name = name;
        Description = description;
        HitDie = hitDie;
    }

    public string Name { get; }

    public string Description { get; }

    public EDiceType HitDie { get; }
}
