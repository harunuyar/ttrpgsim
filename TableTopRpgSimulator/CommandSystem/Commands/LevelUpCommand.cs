namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Characters;
using DnD.Entities.Classes;

internal class LevelUpCommand : ICommand
{
    public LevelUpCommand(Player player, Class dndClass)
    {
        Player = player;
        DndClass = dndClass;
    }

    public Player Player { get; }

    public Class DndClass { get; }

    public ICommandResult Execute()
    {
        Level? currentLevel = Player.Levels.Where(l => l.Class == DndClass).FirstOrDefault();
        if (currentLevel == null)
        {
            currentLevel = new Level(DndClass, 1);
            Player.Levels.Add(currentLevel);
        }
        else
        {
            currentLevel.LevelNum++;
        }

        ICommand calculateHitPoints = new CalculateHitPointsCommand(Player, DndClass);
        ICommandResult hitPointsResult = calculateHitPoints.Execute();

        if (hitPointsResult.IsSuccess && hitPointsResult is IntegerResult valueResult)
        {
            Player.HitPoints.AddMaxHitPoints(valueResult.Value);
            return EventResult.Success(this);
        }
        else
        {
            return EventResult.Failure(this, "Failed to calculate hit points: " + hitPointsResult.Message);
        }
    }
}
