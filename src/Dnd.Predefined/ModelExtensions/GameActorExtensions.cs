namespace Dnd.Predefined.ModelExtensions;

using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public static class GameActorExtensions
{
    public static bool IsMulticlassSpellcaster(this IGameActor gameActor)
    {
        return gameActor.LevelInfo.GetClasses().Count(c => c.IsSpellcaster(gameActor.LevelInfo.GetLevelsInClass(c.ClassModel)) || (gameActor.LevelInfo.GetSubclassForClass(c.ClassModel)?.IsSpellcaster() ?? false)) > 1;
    }

    public static bool IsSpellcaster(this IClassInstance classInstance, int level)
    {
        return classInstance.ClassModel.Spellcasting?.Level is not null && classInstance.ClassModel.Spellcasting.Level.Value >= level;
    }

    public static bool IsSpellcaster(this ISubclassInstance subclassInstance)
    {
        return subclassInstance.Spellcasting is not null;
    }
}
