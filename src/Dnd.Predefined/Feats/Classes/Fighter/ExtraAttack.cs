namespace Dnd.Predefined.Feats.Classes.Fighter;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.Entities.Actions.BaseActions;

public class ExtraAttack : AFeat
{
    public ExtraAttack(int level) : base("Extra Attack", "You can attack twice, instead of once, whenever you take the Attack action on your turn.")
    {
        Level = level;
    }

    public int Level { get; }

    public int AttacksThisRound { get; private set; }

    public override void HandleCommand(ICommand command)
    {
        if (command is RollAttack)
        {
            AttacksThisRound++;
        }
        else if (command is TakeTurn)
        {
            AttacksThisRound = 0;
        }
        else if (command is GetActions getActions)
        {
            if (AttacksThisRound == Level)
            {
                getActions.AddFinalAction(new Action(() =>
                {
                    foreach (var item in getActions.Result.Values.Where(x => x is IAttackAction && x is AAction).Select(x => x as AAction))
                    {
                        item!.ActionType = System.Entities.Actions.EActionType.FreeAction;
                    }
                }));
            }
        }
    }
}
