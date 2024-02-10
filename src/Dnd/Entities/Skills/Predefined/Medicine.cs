﻿namespace Dnd.Entities.Skills.Predefined;

using Dnd.Entities.Attributes;

public class Medicine : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Wisdom;

    public string Name => "Medicine";

    public string Description => "A Wisdom (Medicine) check lets you try to stabilize a dying companion or diagnose an illness.";

    private Medicine() { }

    public static readonly Medicine Instance = new Medicine();
}