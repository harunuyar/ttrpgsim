namespace DnD.Entities.Skills;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using TableTopRpg.Entities.Character;

internal class Skill : ISkill
{
    public static readonly Skill Acrobatics = new Skill("Acrobatics", "Performing acrobatic feats such as jumps or rolls", EAttributeType.Dexterity);
    public static readonly Skill AnimalHandling = new Skill("Animal Handling", "Calm down a domesticated animal or train a wild one", EAttributeType.Wisdom);
    public static readonly Skill Arcana = new Skill("Arcana", "Recall lore about spells, magic items, eldritch symbols, magical traditions, the planes of existence, and the inhabitants of those planes", EAttributeType.Intelligence);
    public static readonly Skill Athletics = new Skill("Athletics", "Climb, jump, or swim", EAttributeType.Strength);
    public static readonly Skill Deception = new Skill("Deception", "Convince others that something false is true", EAttributeType.Charisma);
    public static readonly Skill History = new Skill("History", "Recall lore about historical events, legendary people, ancient kingdoms, past disputes, recent wars, and lost civilizations", EAttributeType.Intelligence);
    public static readonly Skill Insight = new Skill("Insight", "Determine the true intentions of a creature", EAttributeType.Wisdom);
    public static readonly Skill Intimidation = new Skill("Intimidation", "Influence others through threats, hostile actions, and physical violence", EAttributeType.Charisma);
    public static readonly Skill Investigation = new Skill("Investigation", "Deduce the location of a hidden object, discern from the appearance of a wound what kind of weapon dealt it, or determine the weakest point in a tunnel that could cause it to collapse", EAttributeType.Intelligence);
    public static readonly Skill Medicine = new Skill("Medicine", "Stabilize a dying companion or diagnose an illness", EAttributeType.Wisdom);
    public static readonly Skill Nature = new Skill("Nature", "Recall lore about terrain, plants, and animals", EAttributeType.Intelligence);
    public static readonly Skill Perception = new Skill("Perception", "Spot, hear, or otherwise detect the presence of something", EAttributeType.Wisdom);
    public static readonly Skill Performance = new Skill("Performance", "Delight an audience with music, dance, acting, or some other form of entertainment", EAttributeType.Charisma);
    public static readonly Skill Persuasion = new Skill("Persuasion", "Influence others through tact, social graces, or good nature", EAttributeType.Charisma);
    public static readonly Skill Religion = new Skill("Religion", "Recall lore about deities, rites and prayers, religious hierarchies, holy symbols, and the practices of secret cults", EAttributeType.Intelligence);
    public static readonly Skill SleightOfHand = new Skill("Sleight of Hand", "Hide an object on your person or pick a pocket", EAttributeType.Dexterity);
    public static readonly Skill Stealth = new Skill("Stealth", "Avoid detection", EAttributeType.Dexterity);
    public static readonly Skill Survival = new Skill("Survival", "Follow tracks, hunt wild game, guide your group through frozen wastelands, identify signs that owlbears live nearby, predict the weather, or avoid quicksand and other natural hazards", EAttributeType.Wisdom);

    private Skill(string name, string description, EAttributeType attributeType)
    {
        this.Name = name;
        this.Description = description;
        this.AttributeType = attributeType;
    }

    public string Name { get; }

    public string Description { get; }

    public EAttributeType AttributeType { get; }

    public int GetModifier(AttributeSet dndAttributeSet, int proficiencyLevel)
    {
        var attribute = dndAttributeSet.GetAttribute(AttributeType);
        return attribute.GetModifier() + proficiencyLevel * 2;
    }
}
