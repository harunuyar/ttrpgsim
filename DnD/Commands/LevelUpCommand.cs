namespace DnD.Commands;

using DnD.Entities.Characters;
using DnD.Entities.Classes;
using DnD.Entities.Classes.Predefined;
using TableTopRpg.Commands;

internal class LevelUpCommand : DndCommand
{
    public LevelUpCommand(Character character, IDndClass dndClass) : base(character)
    {
        DndClass = dndClass;
    }

    public IDndClass DndClass { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        Level? currentLevel = Character.Levels.Where(l => l.Class == DndClass).FirstOrDefault();
        if (currentLevel == null)
        {
            if (!CheckAttributeScorePreRequisite(Character.AttributeSet, DndClass))
            {
                return EventResult.Failure(this, "Character does not meet the attribute score pre-requisite for the class");
            }

            currentLevel = new Level(DndClass, 1);
            Character.Levels.Add(currentLevel);
        }
        else
        {
            currentLevel.LevelNum++;
        }

        ICommand calculateHitPoints = new CalculateHitPointsCommand(Character, DndClass);
        ICommandResult hitPointsResult = calculateHitPoints.Execute();

        if (hitPointsResult.IsSuccess && hitPointsResult is IntegerResult valueResult)
        {
            Character.HitPoints.AddMaxHitPoints(valueResult.Value);
            return EventResult.Success(this);
        }
        else
        {
            return EventResult.Failure(this, "Failed to calculate hit points: " + hitPointsResult.Message);
        }
    }

    private static bool CheckAttributeScorePreRequisite(AttributeSet attributeSet, IDndClass dndClass)
    {
        return dndClass switch
        {
            Artificer => attributeSet.Intelligence.Value >= 13,
            Barbarian => attributeSet.Strength.Value >= 13,
            Bard => attributeSet.Charisma.Value >= 13,
            Cleric => attributeSet.Wisdom.Value >= 13,
            Druid => attributeSet.Wisdom.Value >= 13,
            Fighter => attributeSet.Strength.Value >= 13 || attributeSet.Dexterity.Value >= 13,
            Monk => attributeSet.Dexterity.Value >= 13 && attributeSet.Wisdom.Value >= 13,
            Paladin => attributeSet.Strength.Value >= 13 && attributeSet.Charisma.Value >= 13,
            Ranger => attributeSet.Dexterity.Value >= 13 && attributeSet.Wisdom.Value >= 13,
            Rogue => attributeSet.Dexterity.Value >= 13,
            Sorcerer => attributeSet.Charisma.Value >= 13,
            Warlock => attributeSet.Charisma.Value >= 13,
            Wizard => attributeSet.Intelligence.Value >= 13,
            _ => true,
        };
    }
}
