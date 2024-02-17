namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.ListCommands;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.Entities.Classes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public abstract class SpellCastingAbility : AFeat
{
    public SpellCastingAbility(string name, string description, IClass dndClass) : base(name, description)
    {
        SpellCasterClass = dndClass;
        Cantrips = new HashSet<ISpell>();
        Spells = new HashSet<ISpell>();
        AvailableSpellSlots = new int[9];
    }

    public IClass SpellCasterClass { get; }

    public int[] AvailableSpellSlots { get; }

    public HashSet<ISpell> Cantrips { get; }

    public HashSet<ISpell> Spells { get; }

    public abstract int MaxKnownCantripsCount(int spellCasterLevel);

    public abstract int MaxKnownSpellsCount(int spellCasterLevel, int attributeModifier);

    public abstract int MaxSpellSlotsCount(int spellCasterLevel, int spellLevel);

    public int GetSpellCasterLevel(IGameActor gameActor)
    {
        return gameActor.LevelInfo.GetLevelsInClass(SpellCasterClass); ;
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is IsSpellCaster isSpellCaster)
        {
            isSpellCaster.SetValue(this, true, "You are a spell caster.");
            return;
        }
        else if (command is GetSpellCasterAttribute getSpellCasterAttribute)
        {
            getSpellCasterAttribute.SetValue(SpellCasterClass.SpellCastingAttribute);
            return;
        }
        else if (command is CanCastKnownSpell canCastKnownSpell)
        {
            if (canCastKnownSpell.Spell.Level == 0 && Cantrips.Contains(canCastKnownSpell.Spell))
            {
                canCastKnownSpell.SetValue(this, true, $"You can cast {canCastKnownSpell.Spell}.");
                return;
            }
            else if (Spells.Contains(canCastKnownSpell.Spell))
            {
                var spellSlots = new GetAvailableSpellSlots(command.Actor, canCastKnownSpell.Spell.Level).Execute();

                if (!spellSlots.IsSuccess)
                {
                    canCastKnownSpell.SetErrorAndReturn("GetAvailableSpellSlots: " + spellSlots.ErrorMessage);
                    return;
                }

                if (spellSlots.Value <= 0)
                {
                    canCastKnownSpell.SetValue(spellSlots.BaseSource ?? this, false, "You don't have spell slots to cast this spell.");
                    return;
                }

                canCastKnownSpell.SetValue(spellSlots.BaseSource ?? this, true, $"You can cast {canCastKnownSpell.Spell}.");
                return;
            }
            else
            {
                canCastKnownSpell.SetValue(this, false, "You don't know this spell.");
                return;
            }
        }
        else if (command is GetMaxKnownCantripsCount getCantripsCount)
        {
            int level = GetSpellCasterLevel(getCantripsCount.Actor);

            if (level < 1 || level > 20)
            {
                getCantripsCount.SetErrorAndReturn("Spell caster level must be between 1 and 20");
                return;
            }

            if (getCantripsCount.Result.BaseValue == 0)
            {
                getCantripsCount.SetBaseValue(this, MaxKnownCantripsCount(level));
            }
            else
            {
                getCantripsCount.SetBaseValue(this, (getCantripsCount.Result.BaseValue + MaxKnownCantripsCount(level)) / 2);
            }
        }
        else if (command is GetMaxKnownSpellsCount getSpellsCount)
        {
            int level = GetSpellCasterLevel(getSpellsCount.Actor);

            if (level < 1 || level > 20)
            {
                getSpellsCount.SetErrorAndReturn("Spell caster level must be between 1 and 20");
                return;
            }

            var getAttributeModifier = new GetAttributeModifier(getSpellsCount.Actor, SpellCasterClass.SpellCastingAttribute);
            var attributeModifier = getAttributeModifier.Execute();

            if (!attributeModifier.IsSuccess)
            {
                getSpellsCount.SetErrorAndReturn("GetAttributeModifier: " + attributeModifier.ErrorMessage);
                return;
            }

            if (getSpellsCount.Result.BaseValue == 0)
            {
                getSpellsCount.SetBaseValue(this, MaxKnownSpellsCount(level, attributeModifier.Value));
            }
            else
            {
                getSpellsCount.SetBaseValue(this, (getSpellsCount.Result.BaseValue + MaxKnownSpellsCount(level, attributeModifier.Value)) / 2);
            }
        }
        else if (command is GetMaxSpellSlotsCount getSpellSlotsCount)
        {
            int level = GetSpellCasterLevel(getSpellSlotsCount.Actor);

            if (level < 1 || level > 20)
            {
                getSpellSlotsCount.SetErrorAndReturn("Spell caster level must be between 1 and 20");
                return;
            }

            if (getSpellSlotsCount.Result.BaseValue == 0)
            {
                getSpellSlotsCount.SetBaseValue(this, MaxSpellSlotsCount(level, getSpellSlotsCount.SpellLevel));
            }
            else
            {
                getSpellSlotsCount.SetBaseValue(this, (getSpellSlotsCount.Result.BaseValue + MaxSpellSlotsCount(level, getSpellSlotsCount.SpellLevel)) / 2);
            }
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
        else if (command is CanLearnSpell canLearnSpell)
        {
            if (canLearnSpell.Spell.Level == 0)
            {
                var getKnownCantripsCount = new GetMaxKnownCantripsCount(canLearnSpell.Actor);
                var cantripsCount = getKnownCantripsCount.Execute();

                if (!cantripsCount.IsSuccess)
                {
                    canLearnSpell.SetErrorAndReturn("GetMaxKnownCantripsCount: " + cantripsCount.ErrorMessage);
                    return;
                }

                if (cantripsCount.Value <= Cantrips.Count)
                {
                    canLearnSpell.SetValue(this, false, "You have reached maximum known cantrips count.");
                    return;
                }

                if (Cantrips.Contains(canLearnSpell.Spell))
                {
                    canLearnSpell.SetValue(this, false, "You already know this cantrip.");
                    return;
                }

                canLearnSpell.SetValue(this, true, $"You can learn {canLearnSpell.Spell}.");
            }
            else
            {
                var spellsCount = new GetMaxKnownSpellsCount(canLearnSpell.Actor).Execute();

                if (!spellsCount.IsSuccess)
                {
                    canLearnSpell.SetErrorAndReturn("GetMaxKnownSpellsCount: " + spellsCount.ErrorMessage);
                    return;
                }

                if (spellsCount.Value <= Spells.Count)
                {
                    canLearnSpell.SetValue(this, false, "You have reached maximum known spells count.");
                    return;
                }

                if (Spells.Contains(canLearnSpell.Spell))
                {
                    canLearnSpell.SetValue(this, false, "You already know this spell.");
                    return;
                }

                var maxSpellSlots = MaxSpellSlotsCount(GetSpellCasterLevel(command.Actor), canLearnSpell.Spell.Level);

                if (maxSpellSlots <= 0)
                {
                    canLearnSpell.SetValue(this, false, "You don't have spell slots to learn this spell.");
                    return;
                }

                canLearnSpell.SetValue(this, true, $"You can learn {canLearnSpell.Spell}.");
            }
        }
        else if (command is LearnSpell learnSpell)
        {
            var canLearn = new CanLearnSpell(learnSpell.Actor, learnSpell.Spell);
            var canLearnResult = canLearn.Execute();

            if (!canLearnResult.IsSuccess)
            {
                learnSpell.SetErrorAndReturn("CanLearnSpell: " + canLearnResult.ErrorMessage);
                return;
            }

            if (!canLearnResult.Value)
            {
                learnSpell.SetErrorAndReturn("You can't learn this spell. " + canLearnResult.Message);
                return;
            }

            if (learnSpell.Spell.Level == 0)
            {
                Cantrips.Add(learnSpell.Spell);
                learnSpell.SetMessage($"{learnSpell.Spell.Name} is added to known cantrips.");
            }
            else
            {
                Spells.Add(learnSpell.Spell);
                learnSpell.SetMessage($"{learnSpell.Spell.Name} is added to known spells.");
            }
        }
        else if (command is LongRest longRest)
        {
            for (int spellLevel = 1; spellLevel <= 9; spellLevel++)
            {
                var getMaxSpellSlotsCount = new GetMaxSpellSlotsCount(command.Actor, spellLevel);
                var maxSpellSlotsCount = getMaxSpellSlotsCount.Execute();

                if (!maxSpellSlotsCount.IsSuccess)
                {
                    longRest.SetErrorAndReturn("GetMaxSpellSlotsCount: " + maxSpellSlotsCount.ErrorMessage);
                    return;
                }

                AvailableSpellSlots[spellLevel - 1] = maxSpellSlotsCount.Value;
            }
        }
    }
}
