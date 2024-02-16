namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class BreakConcentration : DndEventCommand
{
    public BreakConcentration(IEventListener eventListener, IGameActor character) : base(eventListener, character)
    {
    }

    protected override void FinalizeResult()
    {
        if (Actor.EffectsTable.Concentration != null)
        {
            Actor.EffectsTable.RemoveConcentration();
            Result.SetMessage($"{Actor.Name} has lost concentration.");
        }
    }
}