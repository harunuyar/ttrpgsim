namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Characters;

public class CalculateWeaponAttackModifierAgainstCharacter : DndScoreCommand
{
    public CalculateWeaponAttackModifierAgainstCharacter(ICharacter character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }

    protected override void FinalizeResult()
    {
    }
}
