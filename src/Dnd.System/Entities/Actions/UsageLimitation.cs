namespace Dnd.System.Entities.Actions;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;

public class UsageLimitation
{
    public UsageLimitation(EUsageLimitation limitation, int? charges)
    {
        Limitation = limitation;
        Charges = charges;
        ChargesLeft = charges;
    }

    public EUsageLimitation Limitation { get; }

    public int? Charges { get; }

    public int? ChargesLeft { get; set; }

    public bool IsAvailable()
    {
        if (ChargesLeft.HasValue)
        {
            return ChargesLeft.Value > 0;
        }

        return true;
    }

    public void Use()
    {
        if (ChargesLeft.HasValue)
        {
            ChargesLeft--;
        }
    }

    public void Reset()
    {
        ChargesLeft = Charges;
    }

    public void HandleCommand(ICommand command)
    {
        if (command is LongRest)
        {
            if ((Limitation & (EUsageLimitation.LongRest | EUsageLimitation.ShortRest | EUsageLimitation.PerTurn)) != EUsageLimitation.None)
            {
                Reset();
            }
        }
        else if (command is ShortRest)
        {
            if ((Limitation & (EUsageLimitation.ShortRest | EUsageLimitation.PerTurn)) != EUsageLimitation.None)
            {
                Reset();
            }
        }
        else if (command is TakeTurn)
        {
            if ((Limitation & EUsageLimitation.PerTurn) != EUsageLimitation.None)
            {
                Reset();
            }
        }
    }
}
