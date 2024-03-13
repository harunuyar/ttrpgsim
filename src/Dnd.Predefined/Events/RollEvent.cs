namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class RollEvent : AEvent, IRollEvent
{
    public RollEvent(string name, IGameActor eventOwner, DicePool dicePool, DicePool modifierDicePool, EAdvantage advantage) 
        : base(name, eventOwner)
    {
        DicePool = dicePool;
        ModifierDicePool = modifierDicePool;
        Advantage = advantage;
    }

    public DicePool DicePool { get; }

    public DicePool ModifierDicePool { get; }

    public EAdvantage Advantage { get; }

    public IEnumerable<DiceRollResult>? RollResults { get; private set; }

    public IEnumerable<DiceRollResult>? ModifierRollResults { get; private set; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        RollResults = DicePool.Rolls.SelectMany(r => Enumerable.Repeat(new DiceRollResult(r.DiceType, Advantage, r.Negative), r.NumberOfDice));
        ModifierRollResults = ModifierDicePool.Rolls.SelectMany(r => Enumerable.Repeat(new DiceRollResult(r.DiceType, EAdvantage.None, r.Negative), r.NumberOfDice));
        return base.RunEvent();
    }
}
