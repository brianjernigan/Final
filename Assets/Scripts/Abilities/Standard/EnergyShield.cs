using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : Ability 
{
    public EnergyShield() : base("Energy Shield", 15)
    {
        IsUnlocked = true;
    }

    public override void Activate(GameObject player, GameObject target)
    {
        Debug.Log("Energy Shield");
        if (UsesRemaining == 0) return;

        player.GetComponent<PlayerHealth>().IsDefending = true;

        EndTurn();
    }
}
