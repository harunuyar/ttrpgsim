namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActor;

public class VoidCommand : ACommand<VoidResult>
{
    public VoidCommand(IGameActor actor) : base(actor)
    {
        Result = VoidResult.Empty();
    }

    public override VoidResult Result { get; }

    public void SetMessage(string message)
    {
        Result.SetMessage(message);
    }
}
