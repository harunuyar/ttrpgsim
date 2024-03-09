namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.VoidCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;

public class ExtraAttack : FeatureInstance
{
    public static async Task<ExtraAttack> Create()
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>(Features.ExtraAttack1);
        return featureModel == null 
            ? throw new InvalidOperationException("ExtraAttack1 feature model is not found") 
            : new ExtraAttack(featureModel);
    }

    private ExtraAttack(FeatureModel featureModel) : base(featureModel)
    {
    }

    public int AttacksThisRound { get; private set; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is TakeTurn)
        {
            AttacksThisRound = 0;
        }
        else if (command is UseAction useAction)
        {
            if (useAction.Action is IAttackAction)
            {
                AttacksThisRound++;
            }
        }
        else if (command is IsActionAvailable isActionAvailable)
        {
            if (isActionAvailable.Action is IAttackAction)
            {
                var fighterClass = await DndContext.Instance.GetObject<ClassModel>(Classes.Fighter);

                if (fighterClass == null)
                {
                    throw new InvalidOperationException($"{Classes.Fighter} class model is not found");
                }

                int fighterLevel = command.Actor.LevelInfo.GetLevelsInClass(fighterClass);

                int maxAttacks = fighterLevel >= 20 ? 4 : fighterLevel >= 11 ? 3 : fighterLevel >= 5 ? 2 : 1;

                if (AttacksThisRound != 0 && AttacksThisRound < maxAttacks)
                {
                    isActionAvailable.SetValue(true, FeatureModel.Name ?? "Extra Attack");
                }
            }
        }
    }
}
