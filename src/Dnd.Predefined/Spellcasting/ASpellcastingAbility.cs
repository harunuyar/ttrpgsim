namespace Dnd.Predefined.Spellcasting;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;
using Dnd._5eSRD.Models.Subclass;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public abstract class ASpellcastingAbility : ISpellcastingAbility
{
    public ASpellcastingAbility(ClassModel classModel, SubclassModel? subclassModel)
    {
        ClassModel = classModel;
        SubclassModel = subclassModel;
    }

    public ClassModel ClassModel { get; }

    public SubclassModel? SubclassModel { get; }

    private readonly Dictionary<SpellModel, List<ISpellAction>> knownSpells = [];

    public List<ISpellAction> GetCantripActions()
    {
        return knownSpells.Values.SelectMany(x => x).Where(s => s.SpellSlot == 0).ToList();
    }

    public List<ISpellAction> GetSpellActions()
    {
        return knownSpells.Values.SelectMany(x => x).Where(s => s.SpellSlot > 0).ToList();
    }

    public abstract Task<int> GetMaxCantripsKnown(IGameActor gameActor);

    public abstract Task<int> GetMaxSpellsKnown(IGameActor gameActor);

    public abstract Task<int> GetMaxSpellSlots(IGameActor gameActor, int spellLevel);

    public bool HasSpell(SpellModel spell, int spellSlot)
    {
        return knownSpells.TryGetValue(spell, out var spellActions) && spellActions.Any(x => x.SpellSlot == spellSlot);
    }

    public void LearnSpell(ISpellAction spellAction)
    {
        if (!knownSpells.TryGetValue(spellAction.Spell, out var spellList))
        {
            spellList = [];
            knownSpells.Add(spellAction.Spell, spellList);
        }

        spellList.Add(spellAction);
    }

    public void UnlearnSpell(SpellModel spell)
    {
        knownSpells.Remove(spell);
    }

    public virtual async Task HandleCommand(ICommand command)
    {
        
        if (command is GetActions getActions)
        {
            getActions.AddValues(GetCantripActions(), $"{(SubclassModel is not null ? SubclassModel.Name : ClassModel.Name)} Spellcasting");
            getActions.AddValues(GetSpellActions(), $"{(SubclassModel is not null ? SubclassModel.Name : ClassModel.Name)} Spellcasting");
        }
        else if (command is GetMaxSpellSlotsCount getMaxSpellSlotsCount)
        {
            if (!command.Actor.IsMulticlassSpellcaster())
            {
                try
                {
                    int maxSpellSlots = await GetMaxSpellSlots(getMaxSpellSlotsCount.Actor, getMaxSpellSlotsCount.SpellLevel);
                    getMaxSpellSlotsCount.SetBaseValue(maxSpellSlots, $"{(SubclassModel is not null ? SubclassModel.Name : ClassModel.Name)} Spellcasting");
                }
                catch (Exception ex)
                {
                    getMaxSpellSlotsCount.SetError(ex.Message);
                }
            }
        }
        else if (command is GetSpellcastingLevel spellcastingLevel)
        {
            var spellcasting = SubclassModel?.Spellcasting ?? ClassModel.Spellcasting;

            if (spellcasting?.Level is null)
            {
                spellcastingLevel.SetError($"Spellcasting level is not set for {ClassModel.Name} {SubclassModel?.Name}");
                return;
            }

            if (spellcasting.Level < 1)
            {
                spellcastingLevel.SetError($"Spellcasting level is wrong: {spellcasting.Level}");
                return;
            }

            int level = command.Actor.LevelInfo.GetLevelsInClass(ClassModel);
            if (level == 0)
            {
                spellcastingLevel.SetError($"{command.Actor.Name} has no levels in {ClassModel.Name} {SubclassModel?.Name}");
                return;
            }

            spellcastingLevel.AddBonus(level / spellcasting.Level.Value, $"{(SubclassModel is not null ? SubclassModel.Name : ClassModel.Name)} Spellcasting ({level}/{spellcasting.Level})");
        }
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
        return Task.CompletedTask;
    }
}
