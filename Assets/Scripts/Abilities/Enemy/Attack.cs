//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/06/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Attack : EnemyAbility
{
    private readonly int _damageAmount;
    
    public Attack(int amount)
    {
        Name = "Attack";
        _damageAmount = amount;
    }

    internal override void Activate(ICharacter enemy, ICharacter player)
    {
        if (enemy.IsStunned)
        {
            EndTurn($"{enemy.Name} is stunned!");
            enemy.IsStunned = false;
            return;
        }
        
        if (enemy.IsConfused)
        {
            enemy.TakeDamage(_damageAmount / 2);
            EndTurn($"{enemy.Name} hurt itself in confusion!");
            enemy.IsConfused = false;
            return;
        }

        if (player.IsHiding)
        {
            EndTurn($"{enemy.Name}'s move missed!");
            player.IsHiding = false;
            return;
        }
        
        player.TakeDamage(_damageAmount);
        EndTurn($"{MoveText}{Name}!");
    }
}
