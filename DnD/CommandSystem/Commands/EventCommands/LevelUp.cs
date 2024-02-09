namespace DnD.CommandSystem.Commands.EventCommands;

using DnD.CommandSystem.Commands.IntegerResultCommands;
using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using DnD.Entities.Classes;
using DnD.Entities.Classes.Predefined;
using TableTopRpg.Commands;

internal class LevelUp : DndCommand
{
    public LevelUp(Character character, IDndClass dndClass, bool fixedHpRoll) : base(character)
    {
        DndClass = dndClass;
        FixedHpRoll = fixedHpRoll;
    }

    public IDndClass DndClass { get; }

    public bool FixedHpRoll { get; }

    public override EventResult IsValid()
    {
        Level? currentLevel = Character.Levels.Where(l => l.Class == DndClass).FirstOrDefault();
        if (currentLevel == null && !CheckAttributeScorePreRequisite(Character.AttributeSet, DndClass))
        {
            return EventResult.Failure(this, "Character does not meet the attribute score pre-requisite for the class");
        }

        return EventResult.Success(this);
    }

    public override ICommandResult Execute()
    {
        Level? currentLevel = Character.Levels.Where(l => l.Class == DndClass).FirstOrDefault();
        if (currentLevel == null)
        {
            currentLevel = new Level(DndClass, 1);
            Character.Levels.Add(currentLevel);
        }
        else
        {
            currentLevel.LevelNum++;
        }

        var makeHitPointRoll = new MakeHitPointRoll(Character, DndClass, FixedHpRoll);
        makeHitPointRoll.CollectBonuses();
        var hitPointsResult = makeHitPointRoll.Execute();

        if (hitPointsResult.IsSuccess)
        {
            var getConstitutionModifier = new GetAttributeModifier(Character, EAttributeType.Constitution);
            getConstitutionModifier.CollectBonuses();
            var constitutionModifierResult = getConstitutionModifier.Execute();

            if (!constitutionModifierResult.IsSuccess)
            {
                return EventResult.Failure(this, "Failed to calculate hit points: " + constitutionModifierResult.ErrorMessage);
            }

            int constitutionBonus = constitutionModifierResult.Value * Character.Level;

            Character.HitPoints.AddHitPointRoll(hitPointsResult.Value, constitutionBonus);
            
            return EventResult.Success(this);
        }
        else
        {
            return EventResult.Failure(this, "Failed to calculate hit points: " + hitPointsResult.ErrorMessage);
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
