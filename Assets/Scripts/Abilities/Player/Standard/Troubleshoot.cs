using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Troubleshoot : PlayerAbility 
{
    public Troubleshoot()
    {
        Name = "Troubleshoot";
        MaxUses = 15;
        UsesRemaining = MaxUses;
        IsUnlocked = true;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("Troubleshoot");
        if (UsesRemaining == 0) return;

        player.CurrentHealth = Mathf.Max(player.MaxHealth, player.CurrentHealth + 5);
        
        EndTurn();
    }
}
