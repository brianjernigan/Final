using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : Ability 
{
    public abstract override void Activate(ICharacter enemy, ICharacter player);

    protected void EndTurn(string turnName)
    {
        BattleManager.Instance.EnemyMoveName = turnName;
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }
}
