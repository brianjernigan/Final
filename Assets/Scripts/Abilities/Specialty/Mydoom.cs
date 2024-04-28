using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mydoom : Ability
{
    public Mydoom() : base("Mydoom", 2)
    {
    }

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
