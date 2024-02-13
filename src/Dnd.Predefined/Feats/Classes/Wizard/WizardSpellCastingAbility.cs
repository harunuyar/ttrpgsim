namespace Dnd.Predefined.Feats.Classes.Wizard;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

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
            Predefined.Classes.Wizard.Instance)
    {
    }

    public override void HandleCantripsCount(GetKnownCantripsCount getKnownCantripsCount)
    {
        int wizardLevel = getKnownCantripsCount.Character.LevelInfo.GetLevel(Predefined.Classes.Wizard.Instance);

        if (wizardLevel < 1 || wizardLevel > 20)
        {
            getKnownCantripsCount.SetErrorAndReturn("Wizard level must be between 1 and 20");
        }

        getKnownCantripsCount.SetBaseValue(this, CantripsKnownPerLevel[wizardLevel - 1]);
    }

    public override void HandleKnownSpellsCount(GetKnownSpellsCount getKnownSpellsCount)
    {
        var getIntelligenceModifier = new GetAttributeModifier(getKnownSpellsCount.Character, EAttributeType.Intelligence);
        var intelligenceModifier = getIntelligenceModifier.Execute();

        if (!intelligenceModifier.IsSuccess)
        {
            getKnownSpellsCount.SetErrorAndReturn("Couldn't get intelligence modifier. GetAttributeModifier: " + intelligenceModifier.ErrorMessage);
        }

        int wizardLevel = getKnownSpellsCount.Character.LevelInfo.GetLevel(Predefined.Classes.Wizard.Instance);

        if (wizardLevel < 1 || wizardLevel > 20)
        {
            getKnownSpellsCount.SetErrorAndReturn("Wizard level must be between 1 and 20");
        }

        getKnownSpellsCount.SetBaseValue(this, Math.Max(1, wizardLevel + intelligenceModifier.Value));
    }

    public override void HandleSpellSlotsCount(GetSpellSlotsCount getSpellSlotsCount)
    {
        int wizardLevel = getSpellSlotsCount.Character.LevelInfo.GetLevel(Predefined.Classes.Wizard.Instance);

        if (wizardLevel < 1 || wizardLevel > 20)
        {
            getSpellSlotsCount.SetErrorAndReturn("Wizard level must be between 1 and 20");
        }

        getSpellSlotsCount.SetBaseValue(this, SpellSlotsPerSpellLevel[wizardLevel - 1][getSpellSlotsCount.SpellLevel - 1]);
    }
}
