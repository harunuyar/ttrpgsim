namespace Dnd.System.Entities.Skills;

using Dnd.System.Entities.Attributes;

public interface ISkill : IDndEntity
{
    string Description { get; }
    public EAttributeType AttributeType { get; }
}
