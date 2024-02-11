﻿namespace Dnd.Predefined.Feats.Fighter.Level1;

using Dnd.System.CommandSystem.Commands;

public class SecondWind : AFeat
{
    public SecondWind() : base("Second Wind", "You can use a bonus action to regain hit points equal to 1d10 + your fighter level. Once per short/long rest.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        // TODO: add Second Wind to GetAvailableActions command with one charge per short/long rest
    }
}