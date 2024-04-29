using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : PlayerAbility
{
    public Hack()
    {
        Name = "Hack";
        MaxUses = 10;
        UsesRemaining = MaxUses;
        IsUnlocked = true;
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
