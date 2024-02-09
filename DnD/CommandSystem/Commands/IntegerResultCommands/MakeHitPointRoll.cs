namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities;
using DnD.Entities.Characters;
using DnD.Entities.Classes;
using DnD.GameManagers.Dice;

internal class MakeHitPointRoll : DndRollCommand
{
    public MakeHitPointRoll(Character character, IDndClass dndClass, bool fixedRoll, EAdvantage? overrideAdvantage = null) : base(character, dndClass.HitDie, overrideAdvantage)
    {
        DndClass = dndClass;
        FixedRoll = fixedRoll;
    }

    public IDndClass DndClass { get; }

    public bool FixedRoll { get; }

    public override void CollectBonuses()
    {
        if (Character.Level == 1)
        {
            FixedRollResult = (int)DndClass.HitDie;
        }
        else if (FixedRoll)
        {
            FixedRollResult = DndClass.HitDie switch
            {
                EDiceType.D6 => 4,
                EDiceType.D8 => 5,
                EDiceType.D10 => 6,
                EDiceType.D12 => 7,
                _ => 0
            };
        }

        base.CollectBonuses();
    }
}
