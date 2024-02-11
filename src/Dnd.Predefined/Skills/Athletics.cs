namespace Dnd.Predefined.Skills;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Skills;

public class Athletics : ISkill
{
    public EAttributeType AttributeType => EAttributeType.Strength;

    public string Name => "Athletics";

    public string Description => "Your Strength (Athletics) check covers difficult situations you encounter while climbing, jumping, or swimming. Examples include the following activities: Climbing, jumping, and swimming. These might be simple tests of strength, or they might be complex tasks that require you to solve a problem to proceed.";

    private Athletics() { }

    public static readonly Athletics Instance = new Athletics();
}
