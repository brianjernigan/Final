using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incognito : Ability
{
    public Incognito() : base("Incognito", 5)
    {
    }

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
