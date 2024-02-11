namespace Dnd.Entities.Skills;

using Dnd.Entities.Attributes;

public interface IDndSkill : IDndEntity
{
    string Description { get; }
    public EAttributeType AttributeType { get; }
}
