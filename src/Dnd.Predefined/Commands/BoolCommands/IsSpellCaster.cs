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
        foreach (var level in Actor.LevelInfo.GetLastLevels())
        {
            var spellcasting = level.ClassInstance.ClassModel.Spellcasting;
            if (spellcasting?.Level != null && (level.LevelModel.LevelNumber ?? 0) >= spellcasting.Level)
            {
                SetValue(true, $"{Actor.Name} is a spell caster.");
                return Task.CompletedTask;
            }
        }

        SetValue(false, $"{Actor.Name} is not a spell caster.");

        return Task.CompletedTask;
    }
}
