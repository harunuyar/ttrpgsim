namespace Dnd.Predefined.GameActor;

using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class PointsCounter : IPointsCounter
{
    private readonly int[] usedSpellSlots = new int[9];

    public int ActionPoints { get; private set; }

    public int BonusActionPoints { get; private set; }

    public int ReactionPoints { get; private set; }

    public void Reset()
    {
        ActionPoints = 1;
        BonusActionPoints = 1;
        ReactionPoints = 1;
    }

    public void AddExtraActionPoint()
    {
        ActionPoints++;
    }

    public void UseActionPoint()
    {
        ActionPoints--;
    }

    public void UseBonusActionPoint()
    {
        BonusActionPoints--;
    }

    public void UseReactionPoint()
    {
        ReactionPoints--;
    }

    public Task HandleCommand(ICommand command)
    {
        if (command is TakeTurn)
        {
            Reset();
        }
        else if (command is LongRest)
        {
            ResetSpellSlots();
        }

        return Task.CompletedTask;
    }

    public void ResetSpellSlots()
    {
        for (int i = 0; i < 9; i++)
        {
            usedSpellSlots[i] = 0;
        }
    }

    public void UseSpellSlot(int spellLevel)
    {
        usedSpellSlots[spellLevel - 1]++;
    }

    public int GetUsedSpellCounts(int spellLevel)
    {
        return usedSpellSlots[spellLevel - 1];
    }
}
