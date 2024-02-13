namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Spells;

public class CalculateSpellAttackModifier : DndScoreCommand
{
    public CalculateSpellAttackModifier(ICharacter character, ISpell spell, ICharacter target) : base(character)
    {
        Spell = spell;
        Target = target;
    }

    public ISpell Spell { get; }

    public ICharacter Target { get; }

    protected override void InitializeResult()
    {
        if (Spell.SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
        {
            Result.SetError("Spell doesn't use attack roll");
            return;
        }

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
            return;
        }

        var calculateSpellAttackModifierAgainstCharacter = new CalculateSpellAttackModifierAgainstCharacter(this.Target);
        var attackModifierAgainstCharacter = calculateSpellAttackModifierAgainstCharacter.Execute();

        if (attackModifierAgainstCharacter.IsSuccess)
        {
            if (attackModifierAgainstCharacter.BaseValue != 0)
            {
                Result.BonusCollection.AddBonus(attackModifierAgainstCharacter.BaseSource ?? new CustomDndEntity("Attack Modifier Base Against Character"), attackModifierAgainstCharacter.Value);
            }

            foreach (var bonus in attackModifierAgainstCharacter.BonusCollection.Values)
            {
                Result.BonusCollection.AddBonus(bonus.Key, bonus.Value);
            }
        }
    }

    protected override void FinalizeResult()
    {
    }
}
