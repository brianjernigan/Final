using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ElectroPulse : PlayerAbility
{
    public ElectroPulse()
    {
        Name = "Electro Pulse";
        MaxUses = 15;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
    }

    public override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;

        if (player.IsCharging)
        {
            EndTurn(ChargingText);
            return;
        }
        
        player.IsDefending = false;
        UsesRemaining--;
        enemy.TakeDamage(5);
        enemy.IsStunned = true;
        
        EndTurn($"{MoveText}{Name}!");
    }
}
