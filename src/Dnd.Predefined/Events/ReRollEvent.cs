namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class ReRollEvent : AEvent, IRollEvent
{
    public ReRollEvent(string name, IGameActor eventOwner, 
        IEnumerable<DiceRollResult> prevRollResults, IEnumerable<DiceRollResult> prevModifierRollResults, EAdvantage advantage) 
        : base(name, eventOwner)
    {
        PreviousRollResults = prevRollResults;
        PreviousModifierRollResults = prevModifierRollResults;
        Advantage = advantage;

        DicePool = new DicePool(prevRollResults.Select(r => new DiceRoll(1, r.DiceType, r.Negative)).ToList(), 0);
        ModifierDicePool = new DicePool(prevModifierRollResults.Select(r => new DiceRoll(1, r.DiceType, r.Negative)).ToList(), 0);
    }

    private IEnumerable<DiceRollResult> PreviousRollResults { get; }

    private IEnumerable<DiceRollResult> PreviousModifierRollResults { get; }

    public EAdvantage Advantage { get; }

    public IEnumerable<DiceRollResult>? RollResults { get; private set; }

    public IEnumerable<DiceRollResult>? ModifierRollResults { get; private set; }

    public DicePool DicePool { get; }

    public DicePool ModifierDicePool { get; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        foreach (var roll in PreviousRollResults)
        {
            roll.Roll();
        }

        foreach (var roll in PreviousModifierRollResults)
        {
            roll.Roll();
        }

        RollResults = PreviousRollResults;
        ModifierRollResults = PreviousModifierRollResults;

        return base.RunEvent();
    }
}
