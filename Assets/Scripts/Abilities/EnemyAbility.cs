using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : Ability 
{
    public abstract override void Activate(ICharacter enemy, ICharacter player);

    protected void EndTurn()
    {
        BattleManager.Instance.EnemyMoveName = Name;
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }
}
