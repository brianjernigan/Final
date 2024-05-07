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

public sealed class EnergyShield : PlayerAbility
{
    public EnergyShield()
    {
        Name = "Energy Shield";
        MaxUses = 20;
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

        player.IsDefending = true;
        UsesRemaining--;
        
        EndTurn($"{MoveText}{Name}!");
    }
}
