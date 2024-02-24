namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

internal class GetAdvantageForDamageRollAgainst : ListCommand<EAdvantage>
{
    public GetAdvantageForDamageRollAgainst(IGameActor actor, IAttackAction attackAction) : base(actor)
    {
        AttackAction = attackAction;
    }

    public IAttackAction AttackAction { get; }
}
