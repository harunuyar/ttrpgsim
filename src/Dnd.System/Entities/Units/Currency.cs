namespace Dnd.System.Entities.Units;

public class Currency
{
    public double Gold { get; set; }

    public double Copper => Gold * 100;

    public double Silver => Gold * 10;

    public double Electrum => Gold * 2;

    public double Platinum => Gold / 10;

    public Currency()
    {
        Gold = 0;
    }

    public Currency(double amount, ECurrencyUnit currency)
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

    public static Currency OfCopper(double amount)
    {
        return new Currency(amount, ECurrencyUnit.Copper);
    }

    public static Currency OfSilver(double amount)
    {
        return new Currency(amount, ECurrencyUnit.Silver);
    }

    public static Currency OfElectrum(double amount)
    {
        return new Currency(amount, ECurrencyUnit.Electrum);
    }

    public static Currency OfGold(double amount)
    {
        return new Currency(amount, ECurrencyUnit.Gold);
    }

    public static Currency OfPlatinum(double amount)
    {
        return new Currency(amount, ECurrencyUnit.Platinum);
    }
}
