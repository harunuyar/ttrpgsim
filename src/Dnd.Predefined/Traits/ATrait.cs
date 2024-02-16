namespace Dnd.Predefined.Traits;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Traits;

public abstract class ATrait : ITrait
{
    public ATrait(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }

    public string Description { get; }

    public virtual void HandleCommand(ICommand command)
    {
    }
}