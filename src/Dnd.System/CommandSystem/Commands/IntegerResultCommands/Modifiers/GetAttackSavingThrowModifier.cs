namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetAttackSavingThrowModifier : GetSavingThrowModifier
{
    public GetAttackSavingThrowModifier(IGameActor character, IGameActor target, IAttackAction attackAction) 
        : base(character, attackAction.SavingThrowAttribute ?? EAttributeType.None, attackAction.DamageType)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IAttackAction AttackAction { get; }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
        base.InitializeResult();

        AttackAction.HandleCommand(this);

        if (Target != null)
        {
            var against = new GetAttackSavingThrowModifierAgainst(Target, Actor, AttackAction).Execute();

            if (!against.IsSuccess)
            {
                SetErrorAndReturn("GetAttackSavingThrowModifierAgainst: " + against.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Saving Throw Modifier From Target", against);
        }
    }
}
