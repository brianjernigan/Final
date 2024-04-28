using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mydoom : Ability
{
    public Mydoom() : base("Mydoom", 2)
    {
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("Mydoom");
        if (UsesRemaining == 0) return;
        
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
