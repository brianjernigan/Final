using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : AbilityData
{
    public abstract override void Activate(ICharacter user, ICharacter target);
}
