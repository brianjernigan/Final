using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPulse : Ability
{
    public ElectroPulse() : base("Electro Pulse", 10)
    {
    }

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
