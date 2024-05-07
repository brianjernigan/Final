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

public class NanoSwarm : PlayerAbility
{
    public sealed override string Name { get; set; }
    public int DamagePerTurn { get; set; }
    
    public NanoSwarm()
    {
        Name = "Nano Swarm";
        MaxUses = 10;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
        DamagePerTurn = 5;
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
        enemy.IsSwarmed = true;
        
        EndTurn($"{MoveText}{Name}!");
    }
}
