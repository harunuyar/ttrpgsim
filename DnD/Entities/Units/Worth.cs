namespace DnD.Entities.Units;
using System;

internal class Worth
{
    public double Gold { get; set; }

    public double Copper => Gold * 100;

    public double Silver => Gold * 10;

    public double Electrum => Gold * 2;

    public double Platinum => Gold / 10;

    public Worth()
    {
        Gold = 0;
    }

    public Worth(double amount, ECurrencyUnit currency)
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

    public static Worth OfCopper(double amount)
    {
        return new Worth(amount, ECurrencyUnit.Copper);
    }

    public static Worth OfSilver(double amount)
    {
        return new Worth(amount, ECurrencyUnit.Silver);
    }

    public static Worth OfElectrum(double amount)
    {
        return new Worth(amount, ECurrencyUnit.Electrum);
    }

    public static Worth OfGold(double amount)
    {
        return new Worth(amount, ECurrencyUnit.Gold);
    }

    public static Worth OfPlatinum(double amount)
    {
        return new Worth(amount, ECurrencyUnit.Platinum);
    }
}
