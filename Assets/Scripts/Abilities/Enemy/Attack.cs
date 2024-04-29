using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyAbility
{
    public Attack()
    {
        Name = "Attack";
    }
    
    public override void Activate(ICharacter enemy, ICharacter player)
    {
        player.TakeDamage(2);
        EndTurn();
    }
}
