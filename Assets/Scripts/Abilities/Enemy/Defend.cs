using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Defend : EnemyAbility
{
    public Defend()
    {
        Name = "Defend";
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
        
        enemy.IsDefending = true;
        EndTurn($"{enemy.Name} used: {Name}!");
    }
}
