using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : Ability
{
    protected int MaxUses { get; set; }
    protected int UsesRemaining { get; set; }
    public bool IsUnlocked { get; set; }
    private const string MoveText = "Player used: ";

    public abstract override void Activate(ICharacter player, ICharacter enemy);

    protected void EndTurn()
    {
        UsesRemaining--;
        BattleManager.Instance.PlayerMoveText = MoveText + Name + "!";
        BattleManager.Instance.PlayerHasTakenTurn = true;
    }

    public override string ToString()
    {
        return $"{Name} {UsesRemaining}/{MaxUses}";
    }
}
