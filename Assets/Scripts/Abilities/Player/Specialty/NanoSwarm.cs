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

    public override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;
        
        player.IsDefending = false;
        enemy.IsSwarmed = true;
        
        EndTurn();
    }
}
