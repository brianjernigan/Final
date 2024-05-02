using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Incognito : PlayerAbility
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

        if (player.IsCharging)
        {
            EndTurn(ChargingText);
            return;
        }
        
        player.IsDefending = false;
        player.IsHiding = true;
        
        EndTurn($"{MoveText}{Name}");
    }
}
