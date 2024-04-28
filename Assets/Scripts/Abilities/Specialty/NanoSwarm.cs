using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoSwarm : Ability 
{
    public NanoSwarm() : base("Nano Swarm", 5)
    {
    }

    public override void Activate(GameObject player, GameObject target)
    {
        player.GetComponent<PlayerHealth>().IsDefending = false;
        
        Debug.Log("Nano Swarm");
        if (UsesRemaining == 0) return;
        
        // TODO
        // Implement move mechanics
        
        EndTurn();
    }
}
