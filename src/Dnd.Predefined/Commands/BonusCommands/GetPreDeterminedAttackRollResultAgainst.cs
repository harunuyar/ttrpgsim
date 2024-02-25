namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

internal class GetPreDeterminedAttackRollResultAgainst : ListCommand<ERollResult>
{
    public GetPreDeterminedAttackRollResultAgainst(IGameActor actor, IAttackRollAction attackRollAction) : base(actor)
    {
        AttackRollAction = attackRollAction;
    }

    public IAttackRollAction AttackRollAction { get; }
}
