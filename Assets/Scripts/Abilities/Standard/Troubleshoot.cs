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

    public override void Activate(GameObject player, GameObject target)
    {
        player.GetComponent<PlayerHealth>().IsDefending = false;
        
        Debug.Log("Troubleshoot");
        if (UsesRemaining == 0) return;

        player.GetComponent<PlayerHealth>().CurrentHealth += 5;
        
        EndTurn();
    }
}
