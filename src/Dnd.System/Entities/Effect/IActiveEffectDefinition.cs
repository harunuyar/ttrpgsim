﻿namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.GameActor;

public interface IActiveEffectDefinition : IEffectDefinition
{
    EEffectActivationTime ActivationTime { get; }
    Task Activate(IGameActor source, IGameActor target);
}