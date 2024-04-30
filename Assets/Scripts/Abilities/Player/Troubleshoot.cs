using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ability", menuName = "Abilities/Player Ability/Troubleshoot")]
public class Troubleshoot : PlayerAbility
{
    public int healAmount;
    public override void Activate(ICharacter user, ICharacter target)
    {
        if (UsesRemaining == 0) return;
        
        user.IsDefending = false;
        user.CurrentHealth = Mathf.Max(user.MaxHealth, user.CurrentHealth + healAmount);
        UsesRemaining--;
    }
}
