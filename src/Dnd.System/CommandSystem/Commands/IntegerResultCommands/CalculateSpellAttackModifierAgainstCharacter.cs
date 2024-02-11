namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Characters;

public class CalculateSpellAttackModifierAgainstCharacter : DndScoreCommand
{
    public CalculateSpellAttackModifierAgainstCharacter(ICharacter character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}