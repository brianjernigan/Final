using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Ability", menuName = "Abilities/Enemy Ability/Defend")]
public class Defend : EnemyAbility
{
    public override void Activate(ICharacter user, ICharacter target)
    {
        if (user.IsStunned)
        {
            user.IsStunned = false;
            return;
        }
        
        user.IsDefending = true;
    }
}
