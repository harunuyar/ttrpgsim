namespace DnD.CommandSystem.Results;

using DnD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TableTopRpg.Commands;

internal class IntegerBonuses : IntegerResult
{
    public IntegerBonuses(ICommand command) : base(command)
    {
        BonusValues = new List<(IBonusProvider Source, int Value)>();
        Advantages = new List<(IBonusProvider Source, EAdvantage Advantage)>();
    }

    public List<(IBonusProvider Source, int Value)> BonusValues { get; }

    public List<(IBonusProvider Source, EAdvantage Advantage)> Advantages { get; }

    public EAdvantage Advantage => Advantages.Count == 0 ? EAdvantage.None : Advantages.Select(a => a.Advantage).Aggregate((a, b) => a | b);

    public override int Value => BonusValues.Sum(x => x.Value);

    public void AddBonus(IBonusProvider source, int value)
    {
        BonusValues.Add((source, value));
    }

    public void AddBonus(string source, int value)
    {
        AddBonus(new BonusProviderImpl(source), value);
    }

    public void AddAdvantage(IBonusProvider source, EAdvantage advantage)
    {
        Advantages.Add((source, advantage));
    }

    public void AddAdvantage(string source, EAdvantage advantage)
    {
        AddAdvantage(new BonusProviderImpl(source), advantage);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, BonusValues.Select(e => e.Source.Name + ": " + e.Value))
            + (Advantage.HasAdvantage() ? Environment.NewLine + "(Advantage)" : Advantage.HasDisadvantage() ? Environment.NewLine + "(Disadvantage)" : "");
    }
}
