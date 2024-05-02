using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : Ability
{
    protected int MaxUses { get; set; }
    public int UsesRemaining { get; protected set; }
    public bool IsUnlocked { get; set; }
    protected const string MoveText = "Player used: ";
    protected const string ChargingText = "Player is charging Mydoom!";

    public abstract override void Activate(ICharacter player, ICharacter enemy);

    public override void EndTurn(string turnText)
    {
        BattleManager.Instance.PlayerMoveText = turnText;
        BattleManager.Instance.PlayerHasTakenTurn = true;
    }

    public override string ToString()
    {
        return $"{Name} {UsesRemaining}/{MaxUses}";
    }
}
