namespace Dnd.System.Entities.Spells;

[Flags]
public enum ESpellComponent
{
    None = 0,
    Verbal = 1,
    Somatic = 2,
    Material = 4,
}
