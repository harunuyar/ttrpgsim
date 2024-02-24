namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

internal class GetAdvantageForAttackRollAgainst : ListCommand<EAdvantage>
{
    public GetAdvantageForAttackRollAgainst(IGameActor actor, IAttackRollAction attackRollAction) : base(actor)
    {
        AttackRollAction = attackRollAction;
    }

    public IAttackRollAction AttackRollAction { get; }
}
