namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class LawfulNeutral : IAlignment
{
    public string Name => "Lawful Neutral";

    public string Description => "A lawful neutral character acts as law, tradition, or a personal code directs them. Order and organization are paramount. They may believe in personal order and live by a code or standard, or they may believe in order for all and favor a strong, organized government. Lawful neutral is the best alignment you can be because it means you are reliable and honorable without being a zealot. Lawful neutral can be a dangerous alignment when it seeks to eliminate all freedom, choice, and diversity in society.";

    private LawfulNeutral() { }

    public static readonly LawfulNeutral Instance = new LawfulNeutral();
}
