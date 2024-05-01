using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : EnemyAbility
{
    public Heal()
    {
        Name = "Heal";
    }

    public override void Activate(ICharacter enemy, ICharacter player)
    {
        if (enemy.IsConfused)
        {
            EndTurn("The enemy is confused!");
            enemy.IsConfused = false;
            return;
        }
        
        enemy.CurrentHealth = Mathf.Max(enemy.MaxHealth, enemy.CurrentHealth + 2);
        EndTurn($"Enemy used: {Name}!");
    }
}
