namespace DnD.Entities.Skills.Predefined;

using DnD.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class AnimalHandling : IDndSkill
{
    public EAttributeType AttributeType => EAttributeType.Wisdom;

    public string Name => "Animal Handling";

    public string Description => "The ability to calm down or train animals";

    private AnimalHandling() { }

    public static readonly AnimalHandling Instance = new AnimalHandling();
}
