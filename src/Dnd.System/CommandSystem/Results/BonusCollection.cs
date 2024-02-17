namespace Dnd.System.CommandSystem.Results;

using Dnd.System.Entities;
using Dnd.System.Entities.DiceModifiers;

public class BonusCollection
{
    public BonusCollection()
    {
        Values = new();
        Advantages = new();
        RollSuccesses = new();
        HitResults = new();
    }

    public List<(IDndEntity Source, int Value)> Values { get; }

    public List<(IDndEntity Source, EAdvantage Advantage)> Advantages { get; }

    public List<(IDndEntity Source, ERollResult RollSuccess)> RollSuccesses { get; }

    public List<(IDndEntity Source, EHitResult HitResult)> HitResults { get; }

    public int TotalValue => Values.Sum(x => x.Value);

    public EAdvantage Advantage => Advantages.Count == 0 ? EAdvantage.None : Advantages.Select(a => a.Advantage).Aggregate((a, b) => a | b);

    public ERollResult RollSuccess => RollSuccesses.Count == 0 ? ERollResult.None : RollSuccesses.Select(a => a.RollSuccess).Aggregate((a, b) => a | b);

    public EHitResult HitResult => HitResults.Count == 0 ? EHitResult.None : HitResults.Select(a => a.HitResult).Aggregate((a, b) => a | b);

    public void AddBonus(IDndEntity source, int value)
    {
        Values.Add((source, value));
    }

    public void AddBonus(string source, int value)
    {
        AddBonus(new CustomDndEntity(source), value);
    }

    public int GetBonus(IDndEntity source)
    {
        return Values.Where(x => x.Source == source).Select(x => x.Value).FirstOrDefault();
    }

    public int GetBonus(string source)
    {
        return GetBonus(new CustomDndEntity(source));
    }

    public void AddAdvantage(IDndEntity source, EAdvantage advantage)
    {
        Advantages.Add((source, advantage));
    }

    public void AddAdvantage(string source, EAdvantage advantage)
    {
        AddAdvantage(new CustomDndEntity(source), advantage);
    }

    public void AddRollSuccess(IDndEntity source, ERollResult rollSuccess)
    {
        RollSuccesses.Add((source, rollSuccess));
    }

    public void AddRollSuccess(string source, ERollResult rollSuccess)
    {
        AddRollSuccess(new CustomDndEntity(source), rollSuccess);
    }

    public void AddHitResult(IDndEntity source, EHitResult hitResult)
    {
        HitResults.Add((source, hitResult));
    }

    public void AddHitResult(string source, EHitResult hitResult)
    {
        AddHitResult(new CustomDndEntity(source), hitResult);
    }

    public void Reset()
    {
        Values.Clear();
        Advantages.Clear();
        RollSuccesses.Clear();
        HitResults.Clear();
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Values.Select(e => e.Source.Name + ": " + e.Value)) 
            + (Advantage.HasAdvantage() ? Environment.NewLine + "(DiceModifiers)" : (Advantage.HasDisadvantage() ? Environment.NewLine + "(Disadvantage)" : string.Empty));
    }
}
