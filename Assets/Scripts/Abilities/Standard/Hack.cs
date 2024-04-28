using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : Ability
{
    public Hack() : base("Hack", 10)
    {
        IsUnlocked = true;
    }

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
