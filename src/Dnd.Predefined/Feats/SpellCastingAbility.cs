namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public abstract class SpellCastingAbility : AFeat
{
    public SpellCastingAbility(string name, string description, EAttributeType spellCasterAttribute) : base(name, description)
    {
        SpellCastingAttribute = spellCasterAttribute;
        Cantrips = new HashSet<ISpell>();
        Spells = new HashSet<ISpell>();
        AvailableSpellSlots = new int[9];
    }

    public EAttributeType SpellCastingAttribute { get; }

    public int[] AvailableSpellSlots { get; }

    public HashSet<ISpell> Cantrips { get; }

    public HashSet<ISpell> Spells { get; }

    public abstract int GetMaxCantripsCount(IGameActor actor);

    public abstract int GetMaxSpellsCount(IGameActor actor);

    public abstract int GetMaxSpellSlotsCount(IGameActor actor, int spellLevel);

    public override void HandleCommand(ICommand command)
    {
        if (command is IsSpellCaster isSpellCaster)
        {
            isSpellCaster.SetValue(this, true);
        }
        else if (command is CanCastKnownSpell canCastKnownSpell)
        {
            if (canCastKnownSpell.Spell.Level == 0 && Cantrips.Contains(canCastKnownSpell.Spell))
            {
                canCastKnownSpell.SetValue(this, true);
            }
            else if (Spells.Contains(canCastKnownSpell.Spell) && AvailableSpellSlots[canCastKnownSpell.Spell.Level - 1] > 0)
            {
                canCastKnownSpell.SetValue(this, true);
            }
        }
        else if (command is CalculateSpellAttackModifier calculateSpellAttackModifier)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Actor, SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellAttackModifier.Result.BaseValue <= attributeModifier.Value)
                {
                    calculateSpellAttackModifier.SetBaseValue(this, attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellAttackModifier.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            var getProficiencyBonus = new GetProficiencyBonus(command.Actor);
            var proficiencyBonus = getProficiencyBonus.Execute();

            if (proficiencyBonus.IsSuccess)
            {
                calculateSpellAttackModifier.AddBonus(new CustomDndEntity("Proficiency Bonus"), proficiencyBonus.Value);
            }
            else
            {
                calculateSpellAttackModifier.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }
        }
        else if (command is CalculateSpellSavingDifficultyClass calculateSpellSavingDifficultyClass)
        {
            var getAttributeModifier = new GetAttributeModifier(command.Actor, SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (attributeModifier.IsSuccess)
            {
                if (calculateSpellSavingDifficultyClass.Result.BaseValue <= attributeModifier.Value)
                {
                    calculateSpellSavingDifficultyClass.SetBaseValue(this, attributeModifier.Value);
                }
            }
            else
            {
                calculateSpellSavingDifficultyClass.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            var getProficiencyBonus = new GetProficiencyBonus(command.Actor);
            var proficiencyBonus = getProficiencyBonus.Execute();

            if (proficiencyBonus.IsSuccess)
            {
                calculateSpellSavingDifficultyClass.AddBonus(new CustomDndEntity("Proficiency Bonus"), proficiencyBonus.Value);
            }
            else
            {
                calculateSpellSavingDifficultyClass.SetErrorAndReturn(proficiencyBonus.ErrorMessage ?? "Couldn't get proficiency bonus");
            }
        }
        else if (command is GetMaxKnownCantripsCount getCantripsCount)
        {
            getCantripsCount.SetBaseValue(this, GetMaxCantripsCount(getCantripsCount.Actor));
        }
        else if (command is GetMaxKnownSpellsCount getSpellsCount)
        {
            getSpellsCount.SetBaseValue(this, GetMaxSpellsCount(getSpellsCount.Actor));
        }
        else if (command is GetMaxSpellSlotsCount getSpellSlotsCount)
        {
            getSpellSlotsCount.SetBaseValue(this, GetMaxSpellSlotsCount(getSpellSlotsCount.Actor, getSpellSlotsCount.SpellLevel));
        }
        else if (command is GetKnownSpells getKnownSpells)
        {
            getKnownSpells.AddItems(Spells);
            getKnownSpells.AddItems(Cantrips);
        }
        else if (command is GetAvailableSpellSlots getAvailableSpellSlots)
        {
            if (getAvailableSpellSlots.SpellLevel < 1 || getAvailableSpellSlots.SpellLevel > 9)
            {
                getAvailableSpellSlots.SetErrorAndReturn("Spell level must be between 1 and 9");
                return;
            }

            getAvailableSpellSlots.SetBaseValue(this, AvailableSpellSlots[getAvailableSpellSlots.SpellLevel - 1]);
        }
        else if (command is AddKnownSpell addKnownSpell)
        {
            if (addKnownSpell.Spell.Level == 0)
            {
                var getKnownCantripsCount = new GetMaxKnownCantripsCount(addKnownSpell.Actor);
                var cantripsCount = getKnownCantripsCount.Execute();

                if (!cantripsCount.IsSuccess)
                {
                    addKnownSpell.SetErrorAndReturn("GetMaxKnownCantripsCount: " + cantripsCount.ErrorMessage);
                    return;
                }

                if (cantripsCount.Value <= Cantrips.Count)
                {
                    addKnownSpell.SetErrorAndReturn("You already know the maximum number of cantrips.");
                    return;
                }

                if (Cantrips.Contains(addKnownSpell.Spell))
                {
                    addKnownSpell.SetErrorAndReturn("You already know this cantrip.");
                    return;
                }

                Cantrips.Add(addKnownSpell.Spell);
                addKnownSpell.SetMessage($"{addKnownSpell.Spell.Name} is added to known cantrips.");
            }
            else
            {
                var getKnownSpellsCount = new GetMaxKnownSpellsCount(addKnownSpell.Actor);
                var spellsCount = getKnownSpellsCount.Execute();

                if (!spellsCount.IsSuccess)
                {
                    addKnownSpell.SetErrorAndReturn("GetMaxKnownSpellsCount: " + spellsCount.ErrorMessage);
                    return;
                }

                if (spellsCount.Value <= Spells.Count)
                {
                    addKnownSpell.SetErrorAndReturn("You already know the maximum number of spells.");
                    return;
                }

                if (Spells.Contains(addKnownSpell.Spell))
                {
                    addKnownSpell.SetErrorAndReturn("You already know this spell.");
                    return;
                }

                Spells.Add(addKnownSpell.Spell);
                addKnownSpell.SetMessage($"{addKnownSpell.Spell.Name} is added to known spells.");
            }
        }
    }
}
