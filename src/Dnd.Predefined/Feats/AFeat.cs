namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Feats;
using Dnd.System.Entities.GameActors;

public abstract class AFeat : IFeat
{
    public AFeat(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name {  get; }

    public virtual string Description { get; }

    public virtual void HandleCommand(ICommand command)
    {
    }

    public virtual bool IsEligible(IGameActor actor)
    {
        return true;
    }
}
