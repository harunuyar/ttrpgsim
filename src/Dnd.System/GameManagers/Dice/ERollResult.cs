﻿namespace Dnd.System.GameManagers.Dice;

[Flags]
public enum ERollResult : byte
{
    None = 0,
    CriticalFailure = 1,
    Failure = 2,
    Success = 4,
    CriticalSuccess = 8,
}
