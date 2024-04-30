using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Ability", menuName = "Abilities/Enemy Ability/Heal")]
public class Heal : EnemyAbility
{
    public int healAmount;

    public override void Activate(ICharacter user, ICharacter target)
    {
        if (user.IsStunned)
        {
            user.IsStunned = false;
            return;
        }
        
        user.IsDefending = false;
        user.CurrentHealth = Mathf.Min(user.MaxHealth, user.CurrentHealth + healAmount);
    }
}
