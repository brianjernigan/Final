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
        enemy.CurrentHealth = Mathf.Max(enemy.MaxHealth, enemy.CurrentHealth + 2);
        EndTurn();
    }
}
