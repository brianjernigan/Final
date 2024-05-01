using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyAbility
{
    public int DamageAmount { get; set; } = 2;
    
    public Attack()
    {
        Name = "Attack";
    }
    
    public override void Activate(ICharacter enemy, ICharacter player)
    {
        if (enemy.IsConfused)
        {
            enemy.TakeDamage(DamageAmount / 2);
            EndTurn("The enemy hurt itself in confusion");
            enemy.IsConfused = false;
            return;
        }
        
        player.TakeDamage(DamageAmount);
        EndTurn(Name);
    }
}
