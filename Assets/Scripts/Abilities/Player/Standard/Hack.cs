using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Hack : PlayerAbility
{
    public Hack()
    {
        Name = "Hack";
        MaxUses = 10;
        UsesRemaining = MaxUses;
        IsUnlocked = true;
    }

    internal override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;

        if (player.IsCharging)
        {
            EndTurn(ChargingText);
            return;
        }
        
        player.IsDefending = false;
        UsesRemaining--;
        enemy.IsConfused = true;

        EndTurn($"{MoveText}{Name}!");
    }
}
