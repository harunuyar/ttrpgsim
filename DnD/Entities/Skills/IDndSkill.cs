namespace DnD.Entities.Skills;

using DnD.Entities.Attributes;
using TableTopRpg.Entities.Character;

internal interface IDndSkill : ISkill
{
    public EAttributeType AttributeType { get; }
}
