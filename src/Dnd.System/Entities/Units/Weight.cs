namespace Dnd.System.Entities.Units;

public class Weight
{
    public double Pounds { get; set; }

    public double Kilograms => Pounds * 0.45359237;

    public Weight()
    {
        Pounds = 0;
    }

    private Weight(double pounds)
    {
        Pounds = pounds;
    }

    public static Weight OfPounds(double amount)
    {
        return new Weight(amount);
    }

    public static Weight OfKilograms(double amount)
    {
        return new Weight(amount * 2.20462);
    }
}
