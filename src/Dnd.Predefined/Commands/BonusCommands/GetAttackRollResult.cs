namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAttackRollResult : ListCommand<ERollResult>
{
    public GetAttackRollResult(IGameActor actor, IAttackRollAction attackRollAction, IGameActor? target, ERollResult defaultRollResult) : base(actor)
    {
        AttackRollAction = attackRollAction;
        Target = target;
        DefaultRollResult = defaultRollResult;
    }

    public IAttackRollAction AttackRollAction { get; }

    public IGameActor? Target { get; }

    public ERollResult DefaultRollResult { get; }

    protected override async Task InitializeResult()
    {
        if (Target is not null)
        {
            var against = await new GetAttackRollResultAgainst(Target, AttackRollAction, DefaultRollResult).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetAttackRollResultAgainst: " + against.ErrorMessage);
                return;
            }

            Add(against);

            against.Values.Select(x => x.Item2).Aggregate((a, b) => a | b);
        }
    }
}
