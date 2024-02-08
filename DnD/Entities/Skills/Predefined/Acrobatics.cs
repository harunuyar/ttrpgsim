namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;
using System;

internal class Acrobatics : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Dexterity;

    public string Name => "Acrobatics";

    public string Description => "Performing acrobatic feats such as jumps or rolls";

    private Acrobatics() { }

    public static readonly Acrobatics Instance = new Acrobatics();
}
