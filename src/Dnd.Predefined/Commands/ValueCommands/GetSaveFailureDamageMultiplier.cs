namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetSaveFailureDamageMultiplier : ValueCommand<double>
{
    public GetSaveFailureDamageMultiplier(IGameActor actor, ISavingThrowDamageAction savingThrowDamageAction) : base(actor)
    {
        SavingThrowDamageAction = savingThrowDamageAction;
    }

    public ISavingThrowDamageAction SavingThrowDamageAction { get; }

    protected override Task InitializeResult()
    {
        SetValue(SavingThrowDamageAction.FailureDamageMultiplier, "Base");
        return Task.CompletedTask;
    }
}
