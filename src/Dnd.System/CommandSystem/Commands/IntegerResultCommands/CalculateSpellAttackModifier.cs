﻿namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities;
using Dnd.Entities.Characters;
using Dnd.Entities.Spells;

public class CalculateSpellAttackModifier : DndScoreCommand
{
    public CalculateSpellAttackModifier(Character character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    public override void InitializeResult()
    {
        if (Spell.SuccessMeasuringType == ESuccessMeasuringType.AttackRoll)
        {
            Result.SetBaseValue("Base", 0);

            // Real base value for spell attack modifier will be provided by spell casting ability feature

            var getProficiencyBonus = new GetProficiencyBonus(this.Character);
            var proficiencyBonus = getProficiencyBonus.Execute();

            if (proficiencyBonus.IsSuccess)
            {
                Result.BonusCollection.AddBonus("Proficiency Bonus", proficiencyBonus.Value);
            }
            else
            {
                Result.SetError(proficiencyBonus.ErrorMessage ?? "Couldn't get proficiency bonus");
            }
        }
        else
        {
            Result.SetError("Spell doesn't use attack roll");
        }
    }
}