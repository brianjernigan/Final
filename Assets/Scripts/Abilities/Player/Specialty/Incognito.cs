using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incognito : PlayerAbility
{
    public Incognito()
    {
        Name = "Incognito";
        MaxUses = 10;
        UsesRemaining = MaxUses;
        IsUnlocked = false;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("Incognito");
        if (UsesRemaining == 0) return;
        
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
