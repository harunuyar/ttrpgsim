namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class AddLevel : VoidCommand
{
    public AddLevel(IGameActor character, LevelInstance level) : base(character)
    {
        Level = level;
    }

    public LevelInstance Level { get; }

    protected override Task InitializeResult()
    {
        int currentLevel = Actor.LevelInfo.GetLevelsInClass(Level.ClassInstance.ClassModel);

        if (currentLevel + 1 != Level.LevelModel.LevelNumber)
        {
            SetError($"{Actor.Name} is not ready to become {Level.LevelModel.Name}.");
        }

        return Task.CompletedTask;
    }

    protected override Task FinalizeResult()
    {
        Actor.LevelInfo.AddLevel(Level);
        SetMessage($"{Actor.Name} has become {Level.LevelModel.Name}.");

        return Task.CompletedTask;
    }
}
