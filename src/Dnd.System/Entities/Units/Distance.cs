namespace Dnd.System.Entities.Units;

public class Distance
{
    public double Feet { get; private set; }

    public double Meters => Feet * 0.3048;

    private Distance(double feet)
    {
        Feet = feet;
    }

    public static Distance OfFeet(double feet) => new Distance(feet);

    public static Distance OfMeters(double meters) => new Distance(meters / 0.3048);
}
