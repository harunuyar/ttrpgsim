namespace Dnd.System.Entities.Units;

public class Weight
{
    public double Pounds { get; set; }
    public double Kilograms => Pounds * 0.45359237;

    public Weight()
    {
        Pounds = 0;
    }

    public Weight(double amount, EWeightUnit weightUnit)
    {
        Pounds = ConvertToPounds(amount, weightUnit);
    }

    private double ConvertToPounds(double amount, EWeightUnit weightUnit)
    {
        return weightUnit switch
        {
            EWeightUnit.Pounds => amount,
            EWeightUnit.Kilograms => amount * 2.20462,
            _ => throw new ArgumentException("Unknown weight unit: " + weightUnit),
        };
    }

    public static Weight OfPounds(double amount)
    {
        return new Weight(amount, EWeightUnit.Pounds);
    }

    public static Weight OfKilograms(double amount)
    {
        return new Weight(amount, EWeightUnit.Kilograms);
    }
}
