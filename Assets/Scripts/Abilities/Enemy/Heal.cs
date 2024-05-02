using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : EnemyAbility
{
    public int healAmount;
    
    public Heal(int amount)
    {
        Name = "Heal";
        healAmount = amount;
    }

    public override void Activate(ICharacter enemy, ICharacter player)
    {
        if (enemy.IsConfused)
        {
            EndTurn($"{enemy.Name} is confused!");
            enemy.IsConfused = false;
            return;
        }
        
        enemy.CurrentHealth = Mathf.Min(enemy.MaxHealth, enemy.CurrentHealth + healAmount);
        EndTurn($"{enemy.Name} used: {Name}!");
    }
}
