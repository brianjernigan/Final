using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : Ability
{
    public Hack() : base("Hack", 10)
    {
        IsUnlocked = true;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("Hack");
        if (UsesRemaining == 0) return;
        
        // TODO
        // Implement move mechanics

        EndTurn();
    }
}
