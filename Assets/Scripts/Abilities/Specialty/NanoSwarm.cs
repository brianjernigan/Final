using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoSwarm : Ability 
{
    public NanoSwarm() : base("Nano Swarm", 5)
    {
    }

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
