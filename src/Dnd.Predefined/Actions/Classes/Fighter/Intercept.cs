namespace Dnd.Predefined.Actions.Classes.Fighter;

using Dnd.Predefined.Effects.Classes.Fighter;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

public class Intercept : AAction
{
    public Intercept() 
        : base(
            "Intercept", 
            "Intercept an attack to someone in 5 feet. Reduce the damage by 1d10 + your proficiency bonus.", 
            EActionType.Reaction, 
            new Range(ERange.Touch, null, null),
            [new UsageLimitation(EUsageLimitation.None, null)])
    {
    }

    public override void Apply(IGameActor actor, IEnumerable<IGameActor> targets)
    {
        int roll = new Random().Next(1, 11);
        var proficiencyBonus = new GetProficiencyBonus(actor).Execute();

        if (!proficiencyBonus.IsSuccess)
        {
            return;
        }

        foreach (var target in targets)
        {
            actor.EffectsTable.AddCausedEffect(new Intercepted(actor, target, roll + proficiencyBonus.Value));
        }
    }
}
