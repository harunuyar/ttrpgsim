namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.GameManagers.Dice;

public interface IAttackSpell : ISpellAction, ITargetingAction, IDamageAction
{
    DicePool DamageAtCharacterLevel(int characterLevel);
    DicePool DamageAtSpellLevel(int spellLevel);
}
