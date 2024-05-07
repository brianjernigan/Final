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

public sealed class Incognito : PlayerAbility
{
    public Incognito()
    {
        Name = "Incognito";
        MaxUses = 10;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
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
        player.IsHiding = true;
        
        EndTurn($"{MoveText}{Name}!");
    }
}
