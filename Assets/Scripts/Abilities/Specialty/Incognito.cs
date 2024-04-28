using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incognito : Ability
{
    public Incognito() : base("Incognito", 5)
    {
    }

    public override void Activate(GameObject player, GameObject target)
    {
        player.GetComponent<PlayerHealth>().IsDefending = false;
        
        Debug.Log("Incognito");
        if (UsesRemaining == 0) return;
        
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
