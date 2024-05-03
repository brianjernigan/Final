using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Defend : EnemyAbility
{
    public Defend()
    {
        Name = "Defend";
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
            EndTurn($"{enemy.Name} is confused!");
            enemy.IsConfused = false;
            return;
        }
        
        if (player.IsHiding)
        {
            EndTurn($"{enemy.Name}'s move missed!");
            player.IsHiding = false;
            return;
        }
        
        enemy.IsDefending = true;
        EndTurn($"{MoveText}{Name}!");
    }
}
