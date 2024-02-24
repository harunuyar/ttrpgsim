namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAttackRollResultAgainst : ListCommand<ERollResult>
{
    public GetAttackRollResultAgainst(IGameActor actor, IAttackRollAction attackRollAction, ERollResult defaultRollResult) : base(actor)
    {
        AttackRollAction = attackRollAction;
        DefaultRollResult = defaultRollResult;
    }

    public IAttackRollAction AttackRollAction { get; }

    public ERollResult DefaultRollResult { get; }
}
