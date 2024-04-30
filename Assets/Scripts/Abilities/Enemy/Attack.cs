using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Ability", menuName = "Abilities/Enemy Ability/Attack")]
public class Attack : EnemyAbility
{
    public int damageAmount;

    public override void Activate(ICharacter user, ICharacter target)
    {
        user.IsDefending = false;
        target.TakeDamage(damageAmount);
    }
}
