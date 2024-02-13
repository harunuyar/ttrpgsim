namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Charmed : AEffect
{
    public Charmed(IGameActor charmer, IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Charmed", "A charmed creature can't attack the charmer or target the charmer with harmful abilities or magical effects. The charmer has advantage on any ability check to interact socially with the creature.", duration, source, target)
    {
        Charmer = charmer;
    }

    public IGameActor Charmer { get; set; }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is CanDoWeaponAttack canDoWeaponAttack && canDoWeaponAttack.Target == Charmer)
        {
            canDoWeaponAttack.SetValue(this, false);
        }
        else if (command is CanDoSpellAttack canDoSpellAttack && canDoSpellAttack.Target == Charmer)
        {
            canDoSpellAttack.SetValue(this, false);
        }
    }
}
