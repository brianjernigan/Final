using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyAbility
{
    public int damageAmount;
    
    public Attack(int amount)
    {
        Name = "Attack";
        damageAmount = amount;
    }
    
    public override void Activate(ICharacter enemy, ICharacter player)
    {
        if (enemy.IsConfused)
        {
            enemy.TakeDamage(damageAmount / 2);
            EndTurn($"{enemy.Name} hurt itself in confusion!");
            enemy.IsConfused = false;
            return;
        }
        
        player.TakeDamage(damageAmount);
        EndTurn($"{enemy.Name} used: {Name}!");
    }
}
