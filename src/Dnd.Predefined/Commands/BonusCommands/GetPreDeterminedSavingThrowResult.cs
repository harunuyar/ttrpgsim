namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetPreDeterminedSavingThrowResult : ListCommand<ERollResult>
{
    public GetPreDeterminedSavingThrowResult(IGameActor actor, ISavingThrowAction savingThrowAction) : base(actor)
    {
        SavingThrowAction = savingThrowAction;
    }

    public ISavingThrowAction SavingThrowAction { get; }

    protected override async Task InitializeResult()
    {
        if (SavingThrowAction.ActionOwner is not null)
        {
            var bonusFromAttacker = await new GetPreDeterminedSavingThrowResultAgainst(SavingThrowAction.ActionOwner, SavingThrowAction, Actor).Execute();

            if (!bonusFromAttacker.IsSuccess)
            {
                SetError("GetAttackSavingThrowModifierAgainst: " + bonusFromAttacker.ErrorMessage);
                return;
            }

            Add(bonusFromAttacker);
        }
    }
}
