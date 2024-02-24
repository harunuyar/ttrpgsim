namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanLearnSpell : ValueCommand<bool>
{
    public CanLearnSpell(IGameActor character, ClassModel classModel, SpellModel spell) : base(character)
    {
        Spell = spell;
        ClassModel = classModel;
    }

    public SpellModel Spell { get; }

    public ClassModel ClassModel { get; }

    protected override async Task InitializeResult()
    {
        if (ClassModel.Spellcasting is null)
        {
            SetValue(false, $"{Actor.Name} can't learn {Spell} because {ClassModel.Name} can't cast spells.");
            return;
        }

        int levelsInClass = Actor.LevelInfo.GetLevelsInClass(ClassModel);

        if (levelsInClass == 0)
        {
            SetValue(false, $"{Actor.Name} can't learn {Spell} because they don't have any levels in {ClassModel.Name}.");
            return;
        }

        var canTakeAnyAction = await new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetError("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Set(canTakeAnyAction);
            ForceComplete();
            return;
        }

        if (Spell.Level is null || Spell.Level < 0 || Spell.Level > 9)
        {
            SetError("Spell level is wrong: " + Spell.Level);
            return;
        }

        if (Spell.Level == 0)
        {
            var maxCantrips = await new GetMaxKnownCantripsCount(Actor, ClassModel).Execute();

            if (!maxCantrips.IsSuccess)
            {
                SetError("GetMaxKnownCantripsCount: " + maxCantrips.ErrorMessage);
                return;
            }

            if (Actor.SpellMemory.GetCantrips(ClassModel).Count >= maxCantrips.Value)
            {
                SetValue(false, $"{Actor.Name} can't learn cantrip {Spell} because they already know the maximum number of cantrips.");
                return;
            }

            SetValue(true, $"{Actor.Name} can learn cantrip {Spell}.");
            return;
        }
        else
        {
            var maxSpells = await new GetMaxKnownSpellsCount(Actor, ClassModel).Execute();

            if (!maxSpells.IsSuccess)
            {
                SetError("GetMaxKnownSpellsCount: " + maxSpells.ErrorMessage);
                return;
            }

            if (Actor.SpellMemory.GetPreparedSpells(ClassModel).Count >= maxSpells.Value)
            {
                SetValue(false, $"{Actor.Name} can't learn spell {Spell} because they already know the maximum number of spells.");
                return;
            }

            SetValue(true, $"{Actor.Name} can learn spell {Spell}.");
            return;
        }
    }
}
