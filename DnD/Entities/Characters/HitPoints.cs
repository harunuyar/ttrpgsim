namespace DnD.Entities.Characters;
using System;

internal class HitPoints
{
    public HitPoints()
    {
        HitPointRolls = new List<int>();
        MaxHitPoints = 0;
        CurrentHitPoints = 0;
        TemporaryHitPoints = 0;
    }

    public List<int> HitPointRolls { get; }
    public int MaxHitPoints { get; private set; }
    public int CurrentHitPoints { get; set; }
    public int TemporaryHitPoints { get; set; }

    public void Heal(int amount)
    {
        CurrentHitPoints = Math.Min(CurrentHitPoints + amount, MaxHitPoints);
    }

    public void Damage(int amount)
    {
        int leftAmountAfterTemporary = Math.Max(amount - TemporaryHitPoints, 0);
        TemporaryHitPoints = Math.Max(TemporaryHitPoints - amount, 0);
        CurrentHitPoints = Math.Max(CurrentHitPoints - leftAmountAfterTemporary, 0);
    }

    public void AddTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = Math.Max(TemporaryHitPoints, amount);
    }

    public void RemoveTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = Math.Max(TemporaryHitPoints - amount, 0);
    }

    public void SetCurrentHitPoints(int amount)
    {
        CurrentHitPoints = amount;
    }

    public void SetTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = amount;
    }

    public void AddHitPointRoll(int roll, int constitutionBonus)
    {
        HitPointRolls.Add(roll);
        MaxHitPoints = constitutionBonus + HitPointRolls.Sum();
    }
}
