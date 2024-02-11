﻿namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Nature : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Intelligence;

    public string Name => "Nature";

    public string Description => "Your Intelligence (Nature) check measures your ability to recall lore about terrain, plants and animals, the weather, and natural cycles";

    private Nature() { }

    public static readonly Nature Instance = new Nature();
}