namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

internal class HasProficiency : ValueCommand<bool>
{
    public HasProficiency(IGameActor actor, IAPIReference reference) : base(actor)
    {
        ProficiencyReference = reference;
    }

    public IAPIReference ProficiencyReference { get; }

    protected override Task InitializeResult()
    {
        if (ProficiencyReference.Url == null)
        {
            SetError($"{ProficiencyReference.Name} equipment url is null.");
            return Task.CompletedTask;
        }

        SetValue(false, "Default");
        return Task.CompletedTask;
    }
}
