using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : EnemyAbility
{
    public Defend()
    {
        Name = "Defend";
    }

    public override void Activate(ICharacter enemy, ICharacter player)
    {
        if (enemy.IsConfused)
        {
            EndTurn("The enemy is confused!");
            enemy.IsConfused = false;
            return;
        }
        
        enemy.IsDefending = true;
        EndTurn($"Enemy used: {Name}");
    }
}
