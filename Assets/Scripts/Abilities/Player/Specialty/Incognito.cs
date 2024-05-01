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

    public override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;
        
        player.IsDefending = false;
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
