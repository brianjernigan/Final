using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPulse : Ability
{
    public ElectroPulse() : base("Electro Pulse", 10)
    {
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
