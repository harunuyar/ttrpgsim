namespace Dnd.Predefined.Actions.Classes.Fighter;

using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;

public class IndomitableReroll : ASelfAction
{
    public IndomitableReroll(int charges) : base("Reroll Saving Throw", "Reroll a saving throw and use the result.", EActionType.FreeAction, [new UsageLimitation(EUsageLimitation.LongRest, charges)])
    {
    }
}
