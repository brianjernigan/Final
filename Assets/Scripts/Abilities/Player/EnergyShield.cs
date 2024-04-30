using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ability", menuName = "Abilities/Player Ability/Energy Shield")]
public class EnergyShield : PlayerAbility
{
    public override void Activate(ICharacter user, ICharacter target)
    {
        if (UsesRemaining == 0) return;
        
        user.IsDefending = true;
        UsesRemaining--;
    }
}
