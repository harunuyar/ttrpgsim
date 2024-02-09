namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities;
using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal abstract class DndCommand : ICommand
{
    public DndCommand(Character character)
    {
        Character = character;
    }

    public Character Character { get; }

    public abstract EventResult IsValid();

    public abstract ICommandResult Execute();
    
    public virtual void CollectBonuses()
    {
        foreach (var trait in Character.Traits)
        {
            if (trait is IBonusProvider bonusProvider)
            {
                 bonusProvider.HandleCommand(this);
            }
        }

        foreach (var feat in Character.Feats)
        {
            if (feat is IBonusProvider bonusProvider)
            {
                bonusProvider.HandleCommand(this);
            }
        }

        foreach (var item in Character.Inventory.Items)
        {
            if (item.ItemDescription is IBonusProvider bonusProvider && item.IsEquipped)
            {
                bonusProvider.HandleCommand(this);
            }
        }

        foreach (var effect in Character.Effects)
        {
            if (effect is IBonusProvider bonusProvider)
            {
                bonusProvider.HandleCommand(this);
            }
        }
    }
}
