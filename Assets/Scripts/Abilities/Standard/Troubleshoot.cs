using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Troubleshoot : Ability 
{
    public Troubleshoot() : base("Troubleshoot", 8)
    {
        IsUnlocked = true;
    }

    public override void Activate(GameObject target)
    {
        if (UsesRemaining == 0) return;
    }
}
