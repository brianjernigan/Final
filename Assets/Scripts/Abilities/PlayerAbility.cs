using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : Ability
{
    public int MaxUses { get; protected set; }
    public int UsesRemaining { get; set; }
    public bool IsUnlocked { get; set; }

    public abstract override void Activate(ICharacter player, ICharacter enemy);

    protected void EndTurn()
    {
        UsesRemaining--;
        BattleManager.Instance.PlayerMoveName = Name;
        BattleManager.Instance.PlayerHasTakenTurn = true;
    }

    public override string ToString()
    {
        return $"{Name} {UsesRemaining}/{MaxUses}";
    }
}
