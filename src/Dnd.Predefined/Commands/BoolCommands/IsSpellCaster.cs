namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class IsSpellCaster : ValueCommand<bool>
{
    public IsSpellCaster(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        foreach (var classModel in Actor.LevelInfo.GetLevels().Select(l => l.ClassInstance.ClassModel))
        {
            if (classModel.Spellcasting?.Level != null && Actor.LevelInfo.GetLevelsInClass(classModel) >= classModel.Spellcasting.Level)
            {
                SetValue(true, $"{Actor.Name} is a spell caster.");
                return Task.CompletedTask;
            }
        }

        SetValue(false, $"{Actor.Name} is not a spell caster.");

        return Task.CompletedTask;
    }
}
