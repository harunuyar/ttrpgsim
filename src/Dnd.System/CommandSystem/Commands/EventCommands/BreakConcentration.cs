namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;

public class BreakConcentration : DndEventCommand
{
    public BreakConcentration(IEventListener eventListener, ICharacter character) : base(eventListener, character)
    {
    }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        if (Character.EffectsTable.Concentration != null)
        {
            Character.EffectsTable.RemoveConcentration();
            EventResult.SetMessage($"{Character.Name} has lost concentration");
        }
    }
}