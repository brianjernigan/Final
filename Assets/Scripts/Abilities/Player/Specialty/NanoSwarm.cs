using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoSwarm : PlayerAbility 
{
    public NanoSwarm()
    {
        Name = "Nano Swarm";
        MaxUses = 10;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        if (UsesRemaining == 0) return;
        
        player.IsDefending = false;
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
