namespace DnD.Entities.Characters;
using System;

internal class HitPoints
{
    public int MaxHitPoints { get; set; }
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

    public void SetMaxHitPoints(int amount)
    {
        MaxHitPoints = amount;
    }

    public void SetCurrentHitPoints(int amount)
    {
        CurrentHitPoints = amount;
    }

    public void SetTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = amount;
    }

    public void AddMaxHitPoints(int amount)
    {
        MaxHitPoints += amount;
    }
}
