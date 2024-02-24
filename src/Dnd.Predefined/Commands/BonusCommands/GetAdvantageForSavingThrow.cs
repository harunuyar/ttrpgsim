namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantageForSavingThrow : ListCommand<EAdvantage>
{
    public GetAdvantageForSavingThrow(IGameActor actor, ISavingThrowAction savingThrowAction) : base(actor)
    {
        SavingThrowAction = savingThrowAction;
    }

    public ISavingThrowAction SavingThrowAction { get; }

    protected override async Task InitializeResult()
    {
        if (SavingThrowAction is ISavingThrowAttackAction savingThrowAttackAction)
        {
            var advantageFromTarget = await new GetAdvantageForAttackSavingThrowAgainst(savingThrowAttackAction.ActionOwner, Actor, savingThrowAttackAction).Execute();

            if (!advantageFromTarget.IsSuccess)
            {
                SetError("GetAdvantageForAttackSavingThrowAgainst: " + advantageFromTarget.ErrorMessage);
                return;
            }

            Add(advantageFromTarget);
        }
    }
}
