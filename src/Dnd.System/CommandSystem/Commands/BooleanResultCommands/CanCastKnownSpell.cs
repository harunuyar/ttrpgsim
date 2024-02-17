namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CanCastKnownSpell : DndBooleanCommand
{
    public CanCastKnownSpell(IGameActor character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void InitializeResult()
    {
        var canTakeAnyAction = new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetErrorAndReturn("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Result.Set(canTakeAnyAction);
            return;
        }

        SetValue(false, $"{Actor.Name} can't cast spell {Spell.Name}.");
    }
}
