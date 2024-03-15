namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetSaveSuccessDamageMultiplier : ValueCommand<double>
{
    public GetSaveSuccessDamageMultiplier(IGameActor actor, ISavingThrowDamageAction savingThrowDamageAction) : base(actor)
    {
        SavingThrowDamageAction = savingThrowDamageAction;
    }

    public ISavingThrowDamageAction SavingThrowDamageAction { get; }

    protected override Task InitializeResult()
    {
        SetValue(SavingThrowDamageAction.SuccessDamageMultiplier, "Base");
        return Task.CompletedTask;
    }
}
