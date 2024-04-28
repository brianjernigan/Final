using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlast : Ability
{
    public LaserBlast() : base("Laser Blast", 20)
    {
        IsUnlocked = true;
    }

    public override void Activate(GameObject player, GameObject target)
    {
        player.GetComponent<PlayerHealth>().IsDefending = false;
        
        Debug.Log("laser blast");
        if (UsesRemaining == 0) return;

        target.GetComponent<Enemy>().TakeDamage(5);

        EndTurn();
    }
}
