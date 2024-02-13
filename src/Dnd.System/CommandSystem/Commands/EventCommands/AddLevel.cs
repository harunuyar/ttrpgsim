namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Levels;

public class AddLevel : DndEventCommand
{
    public AddLevel(IEventListener eventListener, ICharacter character, ILevel level) : base(eventListener, character)
    {
        Level = level;
    }

    public ILevel Level { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        Character.LevelInfo.AddLevel(Level);
        EventResult.SetMessage($"{Character.Name} has become {Level.Name}");
    }
}
