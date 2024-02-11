namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Charmed : AEffect
{
    public Charmed(ICharacter charmer, IEffectDuration duration)
        : base("Charmed", "A charmed creature can't attack the charmer or target the charmer with harmful abilities or magical effects. The charmer has advantage on any ability check to interact socially with the creature.", duration)
    {
        Charmer = charmer;
    }

    public ICharacter Charmer { get; set; }

    public override void HandleCommand(DndCommand command)
    {
        base.HandleCommand(command);

        if (command is CanDoWeaponAttack canDoWeaponAttack && canDoWeaponAttack.Target == Charmer)
        {
            canDoWeaponAttack.Result.SetValue(this, false);
        }
        else if (command is CanDoSpellAttack canDoSpellAttack && canDoSpellAttack.Target == Charmer)
        {
            canDoSpellAttack.Result.SetValue(this, false);
        }
    }
}
