namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class AddLevel : VoidCommand
{
    public AddLevel(IGameActor character, ILevelInstance level, int hitDieRoll) : base(character)
    {
        Level = level;
        HitDieRoll = hitDieRoll;
    }

    public ILevelInstance Level { get; }

    public int HitDieRoll { get; }

    protected override Task InitializeResult()
    {
        int currentLevel = Actor.LevelInfo.GetLevelsInClass(Level.ClassInstance.ClassModel);

        if (currentLevel + 1 != Level.LevelModel.LevelNumber)
        {
            SetError($"{Actor.Name} is not ready to become {Level.LevelModel.Name}.");
            return Task.CompletedTask;
        }

        Actor.HitPoints.AddHitPointRoll(HitDieRoll);

        return Task.CompletedTask;
    }

    protected override Task FinalizeResult()
    {
        Actor.LevelInfo.AddLevel(Level);
        SetMessage($"{Actor.Name} has become {Level.LevelModel.Name}.");

        return Task.CompletedTask;
    }
}
