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

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
