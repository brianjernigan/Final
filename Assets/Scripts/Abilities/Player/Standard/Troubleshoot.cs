using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Troubleshoot : PlayerAbility
{
    private int healAmount;
    
    public Troubleshoot()
    {
        Name = "Troubleshoot";
        MaxUses = 15;
        healAmount = 5;
        UsesRemaining = MaxUses;
        IsUnlocked = true;
    }

    internal override void Activate(ICharacter player, ICharacter enemy)
    {
        if (UsesRemaining == 0) return;

        if (player.IsCharging)
        {
            EndTurn(ChargingText);
            return;
        }
        
        player.IsDefending = false;
        UsesRemaining--;
        player.CurrentHealth = Mathf.Min(player.MaxHealth, player.CurrentHealth + healAmount);
        
        EndTurn($"{MoveText}{Name}!");
    }
}
