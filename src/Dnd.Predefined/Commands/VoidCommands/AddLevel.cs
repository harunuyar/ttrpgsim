namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class AddLevel : VoidCommand
{
    public AddLevel(IGameActor character, ILevelInstance level) : base(character)
    {
        Level = level;
    }

    public ILevelInstance Level { get; }

    protected override Task InitializeResult()
    {
        int currentLevel = Actor.LevelInfo.GetLevelsInClass(Level.ClassInstance.ClassModel);

        if (currentLevel + 1 != Level.LevelModel.LevelNumber)
        {
            SetError($"{Actor.Name} is not ready to become {Level.LevelModel.Name}.");
            return Task.CompletedTask;
        }

        if (!Level.ClassInstance.ClassModel.HitDie.HasValue)
        {
            SetError($"Class {Level.ClassInstance.ClassModel.Name} doesn't have a hit die.");
            return Task.CompletedTask;
        }

        Actor.HitPoints.AddHitPointRoll(Level.ClassInstance.ClassModel.HitDie.Value);

        return Task.CompletedTask;
    }

    protected override Task FinalizeResult()
    {
        Actor.LevelInfo.AddLevel(Level);
        SetMessage($"{Actor.Name} has become {Level.LevelModel.Name}.");

        return Task.CompletedTask;
    }
}
