using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : Ability 
{
    public abstract override void Activate(ICharacter enemy, ICharacter player);

    protected void EndTurn(string turnText)
    {
        BattleManager.Instance.EnemyMoveText = turnText;
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }
}
