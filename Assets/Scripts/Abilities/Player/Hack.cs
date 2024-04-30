using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ability", menuName = "Abilities/Player Ability/Hack")]
public class Hack : PlayerAbility
{
    public override void Activate(ICharacter user, ICharacter target)
    {
        target.IsStunned = true;
    }
}
