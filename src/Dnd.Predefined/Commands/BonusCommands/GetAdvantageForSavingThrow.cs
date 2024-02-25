namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
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
        if (SavingThrowAction.ActionOwner is not null)
        {
            var advantageFromTarget = await new GetAdvantageForAttackSavingThrowAgainst(SavingThrowAction.ActionOwner, Actor, SavingThrowAction).Execute();

            if (!advantageFromTarget.IsSuccess)
            {
                SetError("GetAdvantageForAttackSavingThrowAgainst: " + advantageFromTarget.ErrorMessage);
                return;
            }

            Add(advantageFromTarget);
        }
    }
}
