namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanCastKnownSpell : ValueCommand<bool>
{
    public CanCastKnownSpell(IGameActor character, SpellModel spell) : base(character)
    {
        Spell = spell;
    }

    public SpellModel Spell { get; }

    protected override async Task InitializeResult()
    {
        var canTakeAnyAction = await new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetError("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Set(canTakeAnyAction);
            ForceComplete();
            return;
        }

        SetValue(false, "Default");
    }
}
