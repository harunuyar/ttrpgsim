﻿namespace Dnd.CommandSystem.Results;

using Dnd.Entities;
using Dnd.Entities.Advantage;
using System;
using System.Collections.Generic;
using System.Linq;

public class BonusCollection
{
    public BonusCollection()
    {
        Values = new();
        Advantages = new();
    }

    public Dictionary<IDndEntity, int> Values { get; }

    public Dictionary<IDndEntity, EAdvantage> Advantages { get; }

    public int TotalValue => Values.Sum(x => x.Value);

    public EAdvantage Advantage => Advantages.Count == 0 ? EAdvantage.None : Advantages.Select(a => a.Value).Aggregate((a, b) => a | b);

    public bool AddBonus(IDndEntity source, int value)
    {
        return Values.TryAdd(source, value);
    }

    public bool AddBonus(string source, int value)
    {
        return AddBonus(new CustomDndEntity(source), value);
    }

    public int GetBonus(IDndEntity source)
    {
        return Values.TryGetValue(source, out var value) ? value : 0;
    }

    public int GetBonus(string source)
    {
        return GetBonus(new CustomDndEntity(source));
    }

    public bool AddAdvantage(IDndEntity source, EAdvantage advantage)
    {
        return Advantages.TryAdd(source, advantage);
    }

    public bool AddAdvantage(string source, EAdvantage advantage)
    {
        return AddAdvantage(new CustomDndEntity(source), advantage);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Values.Select(e => e.Key.Name + ": " + e.Value)) 
            + (Advantage.HasAdvantage() ? Environment.NewLine + "(Advantage)" : (Advantage.HasDisadvantage() ? Environment.NewLine + "(Disadvantage)" : string.Empty));
    }
}