namespace Dnd.Entities;

public class CustomDndEntity : IDndEntity
{
    public CustomDndEntity(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public override bool Equals(object? obj)
    {
        return obj is CustomDndEntity entity &&
               Name == entity.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}
