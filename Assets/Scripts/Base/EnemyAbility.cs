//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/06/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : Ability
{
    protected const string MoveText = "Enemy used: ";
    internal abstract override void Activate(ICharacter enemy, ICharacter player);

    public override void EndTurn(string turnText)
    {
        BattleManager.Instance.EnemyMoveText = turnText;
        BattleManager.Instance.EnemyHasTakenTurn = true;
    }
}
