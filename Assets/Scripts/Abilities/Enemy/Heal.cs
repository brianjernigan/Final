using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Heal : EnemyAbility
{
    private readonly int _healAmount;
    
    public Heal(int amount)
    {
        Name = "Heal";
        _healAmount = amount;
    }

    public override void Activate(ICharacter enemy, ICharacter player)
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
        
        enemy.CurrentHealth = Mathf.Min(enemy.MaxHealth, enemy.CurrentHealth + _healAmount);
        EndTurn($"{MoveText}{Name}!");
    }
}
