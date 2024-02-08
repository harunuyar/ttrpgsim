namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;

internal class Stealth : IDndSkill
{ 
    public EAttributeType AttributeType => EAttributeType.Dexterity;

    public string Name => "Stealth";

    public string Description => "Make Dexterity (Stealth) checks to avoid detection, slip past guards, slip away without being noticed, and sneak up on people without being seen or heard.";

    private Stealth() { }

    public static readonly Stealth Instance = new Stealth();
}
