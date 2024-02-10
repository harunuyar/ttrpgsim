namespace DnD.Entities.Races;

internal class Size : ISize
{
    public static readonly Size Tiny = new Size("Tiny", "Tiny");
    public static readonly Size Small = new Size("Small", "Small");
    public static readonly Size Medium = new Size("Medium", "Medium");
    public static readonly Size Large = new Size("Large", "Large");
    public static readonly Size Huge = new Size("Huge", "Huge");
    public static readonly Size Gargantuan = new Size("Gargantuan", "Gargantuan");

    public Size(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public string Name { get; }

    public string Description { get; }
}
