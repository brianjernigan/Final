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
        enemy.IsDefending = true;
        EndTurn();
    }
}
