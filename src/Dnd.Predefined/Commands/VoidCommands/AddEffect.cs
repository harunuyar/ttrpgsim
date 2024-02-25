namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class AddEffect : VoidCommand
{
    public AddEffect(IGameActor character, IEffect effect) : base(character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override Task FinalizeResult()
    {
        if (Effect is IPersonalEffect personalEffect)
        {
            personalEffect.Source.EffectsTable.AddCausedPersonalEffect(personalEffect);
            SetMessage($"Applied {personalEffect.Name} to {personalEffect.Target.Name}.");
        }
        else if (Effect is IAreaEffect areaEffect)
        {
            areaEffect.Source.EffectsTable.AddActiveAreaEffect(areaEffect);
        }
        
        return Task.CompletedTask;
    }
}
