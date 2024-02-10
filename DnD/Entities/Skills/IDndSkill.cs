namespace DnD.Entities.Skills;

using DnD.Entities.Attributes;

internal interface IDndSkill : IDndEntity
{
    string Description { get; }
    public EAttributeType AttributeType { get; }
}
