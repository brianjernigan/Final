using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Ability
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int MaxUses { get; protected set; }
    public int UsesRemaining { get; set; }
    public bool IsUnlocked { get; set; }
    public abstract void Activate(ICharacter player, ICharacter target);

    protected void EndTurn()
    {
        UsesRemaining--;
        BattleManager.Instance.PlayerMoveName = Name;
        BattleManager.Instance.PlayerHasTakenTurn = true;
    }

    protected Ability(string name, int maxUses)
    {
        Name = name;
        MaxUses = maxUses;
        UsesRemaining = MaxUses;
    }

    public override string ToString()
    {
        return $"{Name}: {UsesRemaining}/{MaxUses}";
    }
}
