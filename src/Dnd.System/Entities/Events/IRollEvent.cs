namespace Dnd.System.Entities.Events;

using Dnd.System.GameManagers.Dice;

public interface IRollEvent : IEvent
{
    // Initialized with constructor
    DicePool DicePool { get; }
    DicePool ModifierDicePool { get; }
    EAdvantage Advantage { get; }

    // Initialized with RunEvent method
    IEnumerable<DiceRollResult>? RollResults { get; }
    IEnumerable<DiceRollResult>? ModifierRollResults { get; }
}
