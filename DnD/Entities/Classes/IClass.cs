namespace DnD.Entities.Classes;

using DnD.GameManagers.Dice;

internal interface IClass : TableTopRpg.Entities.Character.IClass
{
    EDiceType HitDie { get; }
}
