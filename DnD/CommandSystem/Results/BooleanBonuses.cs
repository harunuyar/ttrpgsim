namespace DnD.CommandSystem.Results;

using DnD.Entities;
using System.Collections.Generic;
using System.Linq;
using TableTopRpg.Commands;

internal class BooleanBonuses : BooleanResult
{
    public BooleanBonuses(ICommand command) : base(command)
    {
        BonusValues = new List<(IBonusProvider Source, bool Value)>();
    }

    public List<(IBonusProvider Source, bool Value)> BonusValues { get; }

    public override bool Value => BonusValues.Any(x => x.Value);

    public void AddBonus(IBonusProvider source, bool value)
    {
        BonusValues.Add((source, value));
    }

    public void AddBonus(string source, bool value)
    {
        AddBonus(new BonusProviderImpl(source), value);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, BonusValues.Select(x => $"{x.Source.Name}: {x.Value}"));
    }
}
