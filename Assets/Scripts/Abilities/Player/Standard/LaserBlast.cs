using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlast : PlayerAbility
{
    public LaserBlast()
    {
        Name = "Laser Blast";
        MaxUses = 20;
        UsesRemaining = MaxUses;
        IsUnlocked = true;
    }

    public override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;
        
        player.IsDefending = false;
        enemy.TakeDamage(10);

        EndTurn();
    }
}
