using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : PlayerAbility
{
    public EnergyShield()
    {
        Name = "Energy Shield";
        MaxUses = 20;
        UsesRemaining = MaxUses;
        IsUnlocked = true;
    }

    public override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;

        player.IsDefending = true;

        EndTurn();
    }
}
