using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Troubleshoot : Ability 
{
    public Troubleshoot() : base("Troubleshoot", 8)
    {
        IsUnlocked = true;
    }

    public override void Activate(ICharacter player, ICharacter target)
    {
        player.IsDefending = false;
        
        Debug.Log("Troubleshoot");
        if (UsesRemaining == 0) return;

        player.Heal(5);
        
        EndTurn();
    }
}
