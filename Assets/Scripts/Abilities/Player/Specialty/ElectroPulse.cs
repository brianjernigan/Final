using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPulse : PlayerAbility
{
    public ElectroPulse()
    {
        Name = "Electro Pulse";
        MaxUses = 15;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("Electro Pulse");
        if (UsesRemaining == 0) return;
        
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
