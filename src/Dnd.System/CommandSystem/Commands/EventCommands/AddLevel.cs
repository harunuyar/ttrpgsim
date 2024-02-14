namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Levels;

public class AddLevel : DndEventCommand
{
    public AddLevel(IEventListener eventListener, IGameActor character, ILevel level) : base(eventListener, character)
    {
        Level = level;
    }

    public ILevel Level { get; }

    protected override void InitializeResult()
    {
        int currentLevel = Actor.LevelInfo.GetLevelsInClass(Level.Class);

        if (currentLevel + 1 != Level.Level)
        {
            SetErrorAndReturn($"{Actor.Name} is not ready to become {Level.Name}");
        }
    }

    protected override void FinalizeResult()
    {
        Actor.LevelInfo.AddLevel(Level);
        Result.SetMessage($"{Actor.Name} has become {Level.Name}");
    }
}
