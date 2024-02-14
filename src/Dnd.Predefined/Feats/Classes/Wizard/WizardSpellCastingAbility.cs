namespace Dnd.Predefined.Feats.Classes.Wizard;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class WizardSpellCastingAbility : SpellCastingAbility
{
    private static readonly int[] CantripsKnownPerLevel = [3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5];

    private static readonly int[][] SpellSlotsPerSpellLevel =
    [
        [2, 0, 0, 0, 0, 0, 0, 0, 0], // Level 1
        [3, 0, 0, 0, 0, 0, 0, 0, 0], // Level 2
        [4, 2, 0, 0, 0, 0, 0, 0, 0], // Level 3
        [4, 3, 0, 0, 0, 0, 0, 0, 0], // Level 4
        [4, 3, 2, 0, 0, 0, 0, 0, 0], // Level 5
        [4, 3, 3, 0, 0, 0, 0, 0, 0], // Level 6
        [4, 3, 3, 1, 0, 0, 0, 0, 0], // Level 7
        [4, 3, 3, 2, 0, 0, 0, 0, 0], // Level 8
        [4, 3, 3, 3, 1, 0, 0, 0, 0], // Level 9
        [4, 3, 3, 3, 2, 0, 0, 0, 0], // Level 10
        [4, 3, 3, 3, 2, 1, 0, 0, 0], // Level 11
        [4, 3, 3, 3, 2, 1, 0, 0, 0], // Level 12
        [4, 3, 3, 3, 2, 1, 1, 0, 0], // Level 13
        [4, 3, 3, 3, 2, 1, 1, 0, 0], // Level 14
        [4, 3, 3, 3, 2, 1, 1, 1, 0], // Level 15
        [4, 3, 3, 3, 2, 1, 1, 1, 0], // Level 16
        [4, 3, 3, 3, 2, 1, 1, 1, 1], // Level 17
        [4, 3, 3, 3, 3, 1, 1, 1, 1], // Level 18
        [4, 3, 3, 3, 3, 2, 1, 1, 1], // Level 19
        [4, 3, 3, 3, 3, 2, 2, 1, 1], // Level 20
    ];

    public WizardSpellCastingAbility() 
        : base("Wizard Spell Casting Ability", 
            "As a student of arcane magic, you have a spellbook containing spells that show the first glimmerings of your true power.",
            EAttributeType.Intelligence)
    {
    }

    public override int GetMaxCantripsCount(IGameActor actor)
    {
        int wizardLevel = actor.LevelInfo.GetLevelsInClass(Predefined.Classes.Wizard.Instance);

        if (wizardLevel < 1 || wizardLevel > 20)
        {
            return 0;
        }

        return CantripsKnownPerLevel[wizardLevel - 1];
    }

    public override int GetMaxSpellsCount(IGameActor actor)
    {
        var getIntelligenceModifier = new GetAttributeModifier(actor, EAttributeType.Intelligence);
        var intelligenceModifier = getIntelligenceModifier.Execute();

        if (!intelligenceModifier.IsSuccess)
        {
            return 0;
        }

        int wizardLevel = actor.LevelInfo.GetLevelsInClass(Predefined.Classes.Wizard.Instance);

        if (wizardLevel < 1 || wizardLevel > 20)
        {
            return 0;
        }

        return Math.Max(1, wizardLevel + intelligenceModifier.Value);
    }

    public override int GetMaxSpellSlotsCount(IGameActor actor, int spellLevel)
    {
        int wizardLevel = actor.LevelInfo.GetLevelsInClass(Predefined.Classes.Wizard.Instance);

        if (wizardLevel < 1 || wizardLevel > 20)
        {
            return 0;
        }

        return SpellSlotsPerSpellLevel[wizardLevel - 1][spellLevel - 1];
    }
}
