using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ability", menuName = "Abilities/Player Ability/Laser Beam")]
public class LaserBeam : PlayerAbility
{
    public int damageAmount;
    public override void Activate(ICharacter user, ICharacter target)
    {
        if (UsesRemaining == 0) return;

        user.IsDefending = false;
        target.TakeDamage(damageAmount);
        UsesRemaining--;
    }
}
