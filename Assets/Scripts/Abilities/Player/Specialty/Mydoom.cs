using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Mydoom : PlayerAbility
{
    public Mydoom()
    {
        Name = "Mydoom";
        MaxUses = 5;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
    }

    public override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;
        
        player.IsDefending = false;

        if (!player.IsCharging)
        {
            player.IsCharging = true;
            UsesRemaining--;
            EndTurn(ChargingText);
        }
        else
        {
            player.IsCharging = false;
            enemy.TakeDamage((int)(enemy.CurrentHealth * 0.75f));
            EndTurn($"{MoveText}{Name}!");
        }
    }
}
