namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Models.Condition;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class AConditionEffect : IEffectDefinition
{
    public AConditionEffect(ConditionModel conditionModel)
    {
        ConditionModel = conditionModel;
    }

    public ConditionModel ConditionModel { get; }

    public string Name => ConditionModel.Name ?? "Unknown Condition";

    public string Description => string.Join(" ", ConditionModel.Desc ?? []);

    public abstract Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner);
}
