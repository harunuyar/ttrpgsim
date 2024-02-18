namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.Impl;

public class UnarmedStrikeAbility : AFeat
{
    public UnarmedStrikeAbility() : base("Unarmed Strike", "Instead of using a weapon to make a melee weapon attack, you can use an unarmed strike.")
    {
        UnarmedStrikeMainHand = new UnarmedStrike(EActionType.MainAction);
        UnarmedStrikeSecondHand = new UnarmedStrike(EActionType.BonusAction);
    }

    public UnarmedStrike UnarmedStrikeMainHand { get; }

    public UnarmedStrike UnarmedStrikeSecondHand { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetActions getPossibleActions)
        {
            getPossibleActions.AddItems(this, [UnarmedStrikeMainHand, UnarmedStrikeSecondHand]);
        }
    }
}
