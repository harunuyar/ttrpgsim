namespace Dnd.System.CommandSystem.Commands.CustomCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Spells;

internal class GetAvailableSpells : ICommand
{
    public GetAvailableSpells(ICharacter character)
    {
        Character = character;
        Result = new ListResult<ISpell>(this);
    }

    public ICharacter Character { get; }

    public bool IsForceCompleted { get; private set; }

    protected ListResult<ISpell> Result { get; }

    public ICommandResult Execute()
    {
        Character.HandleCommand(this);
        return Result;
    }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    public void AddSpell(ISpell spell)
    {
        if (!IsForceCompleted)
        {
            Result.Values.Add(spell);
        }
    }

    public void SetErrorAndReturn(string errorMessage)
    {
        if (!IsForceCompleted)
        {
            Result.Values.Clear();
            Result.SetError(errorMessage);
            ForceComplete();
        }
    }
}
