namespace Dnd.Predefined.Feats.Proficiency;

public static class ProficiencyExtensions
{
    public static string ToString(this EProficiencyType proficiencyLevel)
    {
        return proficiencyLevel switch
        {
            EProficiencyType.None => "No Proficiency",
            EProficiencyType.HalfProficiency => "Half Proficiency",
            EProficiencyType.FullProficiency => "Full Proficiency",
            EProficiencyType.Expertise => "Expertise",
            _ => throw new ArgumentOutOfRangeException(nameof(proficiencyLevel), proficiencyLevel, null)
        };
    }

    public static float GetMultiplier(this EProficiencyType proficiencyLevel)
    {
        return proficiencyLevel switch
        {
            EProficiencyType.None => 0,
            EProficiencyType.HalfProficiency => 0.5f,
            EProficiencyType.FullProficiency => 1,
            EProficiencyType.Expertise => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(proficiencyLevel), proficiencyLevel, null)
        };
    }

    public static int GetProficiencyModifier(this EProficiencyType proficiencyLevel, int proficiencyBonus)
    {
        return (int)Math.Floor(GetMultiplier(proficiencyLevel) * proficiencyBonus);
    }
}
