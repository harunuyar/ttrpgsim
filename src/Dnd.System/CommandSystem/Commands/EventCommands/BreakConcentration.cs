namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;

public class BreakConcentration : DndEventCommand
{
    public BreakConcentration(IEventListener eventListener, ICharacter character) : base(eventListener, character)
    {
    }

    public override void FinalizeEvent()
    {
        if (Character.EffectsTable.Concentration != null)
        {
            Character.EffectsTable.RemoveConcentration();
            EventResult.SetMessage($"{Character.Name} has lost concentration");
        }
    }
}