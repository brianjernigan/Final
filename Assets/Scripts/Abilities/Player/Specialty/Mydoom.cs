//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/06/2024
/////////////////////////////////////////////

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

    internal override void Activate(ICharacter player, ICharacter enemy)
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
            var damageAmount = Mathf.Max(20, (int)(enemy.CurrentHealth * 0.75));
            enemy.TakeDamage(damageAmount);
            EndTurn($"{MoveText}{Name}!");
        }
    }
}
