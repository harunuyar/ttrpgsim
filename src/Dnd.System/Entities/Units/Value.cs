namespace Dnd.System.Entities.Units;
using System;

public class Value
{
    public double Gold { get; set; }

    public double Copper => Gold * 100;

    public double Silver => Gold * 10;

    public double Electrum => Gold * 2;

    public double Platinum => Gold / 10;

    public Value()
    {
        Gold = 0;
    }

    public Value(double amount, ECurrencyUnit currency)
    {
        SetAmount(amount, currency);
    }

    public void SetAmount(double amount, ECurrencyUnit currency)
    {
        Gold = ConvertToGold(amount, currency);
    }

    public void AddAmount(double amount, ECurrencyUnit currency)
    {
        Gold += ConvertToGold(amount, currency);
    }

    public void SubtractAmount(double amount, ECurrencyUnit currency)
    {
        Gold -= ConvertToGold(amount, currency);
    }

    private double ConvertToGold(double amount, ECurrencyUnit currency)
    {
        return currency switch
        {
            ECurrencyUnit.Copper => amount / 100,
            ECurrencyUnit.Silver => amount / 10,
            ECurrencyUnit.Electrum => amount / 2,
            ECurrencyUnit.Gold => amount,
            ECurrencyUnit.Platinum => amount * 10,
            _ => throw new ArgumentException("Unknown currency: " + currency)
        };
    }

    public static Value OfCopper(double amount)
    {
        return new Value(amount, ECurrencyUnit.Copper);
    }

    public static Value OfSilver(double amount)
    {
        return new Value(amount, ECurrencyUnit.Silver);
    }

    public static Value OfElectrum(double amount)
    {
        return new Value(amount, ECurrencyUnit.Electrum);
    }

    public static Value OfGold(double amount)
    {
        return new Value(amount, ECurrencyUnit.Gold);
    }

    public static Value OfPlatinum(double amount)
    {
        return new Value(amount, ECurrencyUnit.Platinum);
    }
}
